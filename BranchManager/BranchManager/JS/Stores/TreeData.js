var TreeDataActions = Reflux.createActions([
	"resize",
	"setMargins"
]);

var treeDataStore = Reflux.createStore({
	listenables: TreeDataActions,
	onSetMargins: function(margins) {
		this.margins = margins;
		this.onResize();
	},
	onResize: function () {
		this.width = $(window).width() - this.margins.left - this.margins.right;
		this.height = $(window).height() - this.margins.top - this.margins.bottom;

		this.redraw();
	},
	redraw: function () {
		if (!this.currentTree) {
			return;
		}

		var tree = d3.layout.tree()
			.size([this.height, this.width])
			.children(node => node.branches.map(item => item.child));

		var nodes = tree.nodes(this.currentTree);
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

		this.width = 0;
		this.height = 0;
		this.margins = {};

		$.ajax({ url: "api/tree" }).done(data => {
			this.currentTree = data[0].roots[0];
			this.redraw();
		});
	},
	getDefaultData: function() {
		return this.result;
	}
});