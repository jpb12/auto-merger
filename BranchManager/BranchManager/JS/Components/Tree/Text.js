BranchManager.Components.Text = React.createClass({
	displayName: 'Text',
	render: function () {
		return (
			React.createElement(
				'text',
				{
					x: 7,
					y: 3.5
				},
				this.props.name))
	}
});