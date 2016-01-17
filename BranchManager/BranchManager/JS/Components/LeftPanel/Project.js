BranchManager.Components.Project = React.createClass({
	displayName: 'Project',
	handleClick: function () {
		BranchManager.Actions.setProject(this.props.project.projectUrl);
	},
	getClassName: function () {
		return this.props.project.projectUrl === this.props.currentProjectUrl ? 'active' : null;
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
});

BranchManager.Components.Project = ReactRedux.connect(state => ({ currentProjectUrl: state.settings.projectUrl }))(BranchManager.Components.Project);