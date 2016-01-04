var MergeConfigActions = Reflux.createActions([
	"refresh"
]);

var MergeConfigStore = Reflux.createStore({
	listenables: MergeConfigActions,
	onRefresh: function () {
		this.load();
	},
	load: function () {
		return $.ajax({ url: "api/tree" }).done(data => {
			this.config = data;
			this.trigger(this.config);
		});
	},
	init: function () {
		this.config = [];
		this.load().done(data => {
			TreeDataActions.setProject(data[0]);
		});
	},
	getDefaultData: function () {
		return this.config;
	}
});