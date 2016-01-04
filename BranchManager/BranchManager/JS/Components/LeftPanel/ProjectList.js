var ProjectList = React.createClass({
	displayName: 'ProjectList',
	mixins: [Reflux.connect(MergeConfigStore, "mergeConfig")],
	getInitialState: function () {
		return { mergeConfig: MergeConfigStore.getDefaultData() };
	},
	render: function () {
		return (
			React.createElement(
				'ul',
				{},
				this.state.mergeConfig.map(project => React.createElement(
					Project,
					{
						key: project.projectUrl,
						project: project
					}))));
	}
})