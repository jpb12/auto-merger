BranchManager.Components.GradientInput = React.createClass({
	displayName: 'GradientInput',
	handleChange: function (e) {
		BranchManager.Actions.setCommits(e.target.value);
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

BranchManager.Components.GradientInput = ReactRedux.connect(state => ({ commits: state.settings.commits }))(BranchManager.Components.GradientInput);