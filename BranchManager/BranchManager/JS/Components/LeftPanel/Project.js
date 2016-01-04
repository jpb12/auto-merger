var Project = React.createClass({
	displayName: 'Project',
	mixins: [Reflux.connect(SettingsStore, "settings")],
	getInitialState: function () {
		return { settings: SettingsStore.getDefaultData() };
	},
	handleClick: function() {
		SettingsActions.setProject(this.props.project);
	},
	getClassName: function() {
		var className = 'project-list';

		if (this.state.settings.projectUrl === this.props.project.projectUrl) {
			className += ' active';
		}

		return className;
	},
	render: function () {
		return (
			React.createElement(
				'li',
				{
					className: this.getClassName(),
					onClick: this.handleClick
				},
				this.props.project.name));
	}
})