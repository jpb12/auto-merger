var MergeConfigActions = Reflux.createActions([
	'refresh',
	'setProject'
]);

var MergeConfigStore = Reflux.createStore({
	listenables: MergeConfigActions,
	onRefresh: function () {
		this.load();
	},
	onSetProject: function (projectUrl) {
		this.projectUrl = projectUrl;
		if (this.config.length > 0) {
			this.updateTree();
		}
	},
	load: function () {
		return $.ajax({ url: "api/tree" }).done(data => {
			this.config = data;
			if (this.projectUrl !== null) {
				this.updateTree();
			}
			this.trigger(this.config);
		});
	},
	updateTree: function () {
		if (this.config.some(project => project.projectUrl === this.projectUrl)) {
			TreeDataActions.setProject(this.config.filter(project => project.projectUrl === this.projectUrl)[0]);
		} else {
			SettingsActions.setProject(this.config[0]);
		}
	},
	init: function () {
		this.config = [];
		this.projectUrl = null;
		this.load();
	},
	getDefaultData: function () {
		return this.config;
	}
});