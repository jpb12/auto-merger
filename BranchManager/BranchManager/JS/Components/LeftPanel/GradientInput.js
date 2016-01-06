var Components = Components || {};

Components.GradientInput = React.createClass({
	displayName: 'GradientInput',
	handleChange: function (e) {
		Store.dispatch(Actions.setCommits(e.target.value));
	},
	render: function () {
		return (
			React.createElement(
				'input',
				{
					type: 'number',
					id: 'gradient-input',
					value: this.props.commits,
					onChange: this.handleChange
				}));
	}
});

Components.GradientInput = ReactRedux.connect(state => ({ commits: state.settings.commits }))(Components.GradientInput);