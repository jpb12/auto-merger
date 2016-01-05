var Components = Components || {};

Components.ProjectList = React.createClass({
	displayName: 'ProjectList',
	render: function () {
		return (
			React.createElement(
				'ul',
				{
					className: 'project-list'
				},
				this.props.config.map(project => React.createElement(
					Components.Project,
					{
						key: project.projectUrl,
						project: project
					}))));
	}
});

Components.ProjectList = ReactRedux.connect(state => ({ config: state.config.data }))(Components.ProjectList);