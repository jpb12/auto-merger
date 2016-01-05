var Components = Components || {};

Components.Project = React.createClass({
	displayName: 'Project',
	handleClick: function () {
		Store.dispatch(Actions.setProject(this.props.project.projectUrl));
	},
	getClassName: function () {
		var className = 'button';

		if (this.props.project.projectUrl === this.props.currentProjectUrl) {
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
});

Components.Project = ReactRedux.connect(state => ({ currentProjectUrl: state.settings.projectUrl }))(Components.Project);