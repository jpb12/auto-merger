BranchManager.Components.ProjectList = React.createClass({
	displayName: 'ProjectList',
	render: function () {
		return (
			React.createElement(
				'ul',
				{
					className: 'project-list'
				},
				this.props.config.map(project => React.createElement(
					BranchManager.Components.Project,
					{
						key: project.projectUrl,
						project: project
					}))));
	}
});

BranchManager.Components.ProjectList = ReactRedux.connect(state => ({ config: state.config.data }))(BranchManager.Components.ProjectList);