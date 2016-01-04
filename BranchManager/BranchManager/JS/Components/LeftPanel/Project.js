var Project = React.createClass({
	displayName: 'Project',
	handleClick: function() {
		SettingsActions.setProject(this.props.project);
	},
	render: function () {
		return (
			React.createElement(
				'li',
				{
					className: 'project-list',
					onClick: this.handleClick
				},
				this.props.project.name));
	}
})