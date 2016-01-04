var TreeDataActions = Reflux.createActions([
	'resize',
	'setMargins',
	'setProject'
]);

var TreeDataStore = Reflux.createStore({
	listenables: TreeDataActions,
	onSetMargins: function(margins) {
		this.margins = margins;
		this.onResize();
	},
	onSetProject: function(tree) {
		this.currentTree = tree;
		this.redraw();
	},
	onResize: function () {
		this.width = $(window).width() - $('#left-panel').width() - this.margins.left - this.margins.right;
		this.height = $(window).height() - this.margins.top - this.margins.bottom;

		this.redraw();
	},
	redraw: function () {
		if (!this.currentTree || !this.width || !this.height || !this.margins) {
			return;
		}

		var tree = d3.layout.tree()
			.size([this.height, this.width])
			.children(node => node.branches.map(item => item.child));

		// d3 trees can only have one root node, so to show the merge tree when there are multiple nodes
		// we add a new parent of all the roots, which we do not show
		var parentNode = {
			branches: this.currentTree.roots.map(node => ({ child: node }))
		};

		var allNodes = tree.nodes(parentNode);
		var nodes = allNodes.slice(1);

		// we need to remove the offset caused by the temporary parent, and then scale the nodes to fill
		// the full svg
		var depth = Math.max.apply(null, nodes.map(node => node.depth));
		nodes.forEach(node => node.y = (node.y - this.width / depth) * (depth / (depth - 1)));

		var links = tree.links(nodes);

		this.result = {
			nodes: nodes,
			links: links
		};

		this.trigger(this.result);
	},
	init: function () {
		this.result = {
			nodes: [],
			links: []
		};
	},
	getDefaultData: function() {
		return this.result;
	}
});