var ProjectList = React.createClass({
	displayName: 'ProjectList',
	render: function () {
		return (
			React.createElement(
				'ul',
				{},
				this.props.config.map(project => React.createElement(
					Project,
					{
						key: project.projectUrl,
						project: project
					}))));
	}
});

ProjectList = ReactRedux.connect(state => ({ config: state.config.data }))(ProjectList);