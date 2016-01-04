var SettingsStore = Reflux.createStore({
	listenables: SettingsActions,
	onSetProject: function (project) {
		this.settings.projectUrl = project.projectUrl;
		MergeConfigActions.setProject(this.settings.projectUrl);
		this.save();
	},
	save: function () {
		localStorage.setItem('BranchManager', JSON.stringify(this.settings));
		this.trigger(this.settings);
	},
	init: function () {
		this.settings = JSON.parse(localStorage.getItem('BranchManager')) || {};
		MergeConfigActions.setProject(this.settings.projectUrl || "");
	},
	getDefaultData: function () {
		return this.settings;
	}
});