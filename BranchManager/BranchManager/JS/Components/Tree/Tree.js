var Tree = React.createClass({
	displayName: 'Tree',
	mixins: [Reflux.connect(TreeDataStore, "treeData")],
	getInitialState: function () {
		return { treeData: TreeDataStore.getDefaultData() };
	},
	handleResize: function () {
		TreeDataActions.resize();
	},
	componentDidMount: function () {
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
						link: link
					})),
				this.state.treeData.nodes.map(node =>
					React.createElement(
						Node,
						{
							key: node.name,
							node: node
						}))));
	}
});