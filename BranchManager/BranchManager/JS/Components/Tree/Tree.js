var Tree = React.createClass({
	displayName: 'Tree',
	mixins: [Reflux.connect(TreeDataStore, "treeData")],
	getInitialState: function () {
		return {
			treeData: {
				nodes: [],
				links: []
			}
		}
	},
	handleResize: function () {
		TreeDataActions.resize();
	},
	componentDidMount: function () {
		TreeDataActions.setMargins(this.props.margins);

		$(window).on('resize', this.handleResize);
	},
	componentWillUnmount: function () {
		$(window).off('resize', this.handleResize);
	},
	render: function () {
		return (
			React.createElement(
				'svg',
				{},
				this.state.treeData.links.map(link => React.createElement(
					Link,
					{
						key: link.target.name,
						link: link,
						diagonal: this.props.diagonal,
						margins: this.props.margins
					})),
				this.state.treeData.nodes.map(node =>
					React.createElement(
						Node,
						{
							key: node.name,
							node: node,
							margins: this.props.margins
						}))));
	}
});