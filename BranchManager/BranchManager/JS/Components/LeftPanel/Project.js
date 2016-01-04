var Project = React.createClass({
	displayName: 'Project',
	handleClick: function() {
		TreeDataActions.setProject(this.props.project);
	},
	render: function () {
		return (
			React.createElement(
				'li',
				{
					onClick: this.handleClick
				},
				this.props.project.name));
	}
})