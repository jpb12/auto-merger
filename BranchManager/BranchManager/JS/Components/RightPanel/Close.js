BranchManager.Components.Close = React.createClass({
	displayName: 'Close',
	handleClick: function() {
		BranchManager.Actions.setActiveNode(null);
	},
	render: function () {
		return (
			React.createElement(
				'span',
				{
					className: 'close fa fa-close',
					onClick: this.handleClick
				}));
	}
})