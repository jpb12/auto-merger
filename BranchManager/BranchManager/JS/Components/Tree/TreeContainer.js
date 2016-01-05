var TreeContainer = React.createClass({
	displayName: 'TreeContainer',
	getTreeData: function () {
		if (this.props.config.every(project => project.projectUrl !== this.props.projectUrl)) {
			Store.dispatch(Actions.setProject(this.props.config[0].projectUrl));
			return {
				nodes: [],
				links: []
			};
		}

		var project = this.props.config.filter(project => project.projectUrl !== this.props.projectUrl)[0]

		var tree = d3.layout.tree()
			.size([this.props.dimensions.height, this.props.dimensions.width])
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
			node.y = (node.y - this.props.dimensions.width / depth) * (depth / (depth - 1));
			node.x += this.props.dimensions.margins.top;
			node.y += this.props.dimensions.margins.left;
		});

		var links = tree.links(nodes);

		return {
			nodes: nodes,
			links: links
		};
	},
	handleResize: function () {
		Store.dispatch(Actions.resize());
	},
	componentDidMount: function () {
		$(window).on('resize', this.handleResize);
	},
	componentWillUnmount: function () {
		$(window).off('resize', this.handleResize);
	},
	render: function () {
		if (this.props.config.length === 0) {
			return React.createElement('div', { id: 'tree' });
		}

		return (
			React.createElement(
				'div',
				{
					id: 'tree'
				},
				React.createElement(Tree, this.getTreeData())));
	}
})

TreeContainer = ReactRedux.connect(
	state => (
		{
			config: state.config.data,
			dimensions: state.dimensions,
			projectUrl: state.settings.projectUrl
		}))(TreeContainer);