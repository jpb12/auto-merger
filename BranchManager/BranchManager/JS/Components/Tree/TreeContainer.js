BranchManager.Components.TreeContainer = React.createClass({
	displayName: 'TreeContainer',
	getTreeData: function () {
		var height = this.state.height - this.state.margins.top - this.state.margins.bottom;
		var width = this.state.width - (this.props.rightPanelVisible ? 500 : 200);
		var contentWidth = width - this.state.margins.left - this.state.margins.right;

		if (this.props.config.every(project => project.projectUrl !== this.props.projectUrl)) {
			BranchManager.Actions.setProject(this.props.config[0].projectUrl);
			return {
				nodes: [],
				links: []
			};
		}

		var project = this.props.config.filter(project => project.projectUrl === this.props.projectUrl)[0]

		var tree = d3.layout.tree()
			.size(this.props.horizontal
				? [height, contentWidth]
				: [contentWidth, height])
			.children(node => node.branches.map(item => item.child));

		// d3 trees can only have one root node, so to show the merge tree when there are multiple nodes
		// we add a new parent of all the roots, which we do not show
		var parentNode = {
			branches: project.roots.map(node => ({ child: node }))
		};

		var allNodes = tree.nodes(parentNode);
		var nodes = allNodes.slice(1);

		// we need to remove the offset caused by the temporary parent, and then scale the nodes to fill
		// the full svg.  We also need to apply the margins
		var depth = Math.max.apply(null, nodes.map(node => node.depth));
		nodes.forEach(node => {
			if (this.props.horizontal) {
				node.y = (node.y - contentWidth / depth) * (depth / (depth - 1));
				node.x += this.state.margins.top;
				node.y += this.state.margins.left;
			} else {
				node.y = (node.y - height / depth) * (depth / (depth - 1));
				node.x += this.state.margins.left;
				node.y += this.state.margins.top;
			}
		});

		var links = tree.links(nodes);

		return {
			nodes: nodes,
			links: links,
			width: width
		};
	},
	handleResize: function () {
		this.setState({
			width: $(window).width(),
			height: $(window).height()
		});
	},
	componentDidMount: function () {
		$(window).on('resize', this.handleResize);
	},
	componentWillUnmount: function () {
		$(window).off('resize', this.handleResize);
	},
	getInitialState: function () {
		return {
			margins: {
				top: 20,
				bottom: 20,
				left: 20,
				right: 100
			},
			width: $(window).width(),
			height: $(window).height()
		};
	},
	render: function () {
		if (this.props.loading) {
			return (React.createElement(
				'div',
				{
					id: 'tree'
				},
				React.createElement(BranchManager.Components.Spinner)));
		}

		return (
			React.createElement(
				'div',
				{
					id: 'tree'
				},
				React.createElement(BranchManager.Components.Tree, this.getTreeData())));
	}
})

BranchManager.Components.TreeContainer = ReactRedux.connect(
	state => (
		{
			config: state.config.data,
			loading: state.config.loading,
			horizontal: state.settings.horizontal,
			projectUrl: state.settings.projectUrl,
			rightPanelVisible: !!state.activeNode
	}))(BranchManager.Components.TreeContainer);