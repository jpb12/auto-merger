BranchManager.Components.Summary = React.createClass({
	displayName: 'Summary',
	wrapInSpan: function(text, className) {
		return React.createElement('span', { className: className }, text);
	},
	formatDateTime: function(date){
		return date.substring(0, 19).replace('T', ' ');
	},
	render: function () {
		return (
			React.createElement(
				'p',
				{},
				'Created' + (this.props.info.creationBranch ? ' from ' : null),
				this.props.info.creationBranch ? this.wrapInSpan(this.props.info.creationBranch, 'bold') : null,
				' at ',
				this.wrapInSpan('r' + this.props.info.creationRevision, 'italics'),
				' on ',
				this.wrapInSpan(this.formatDateTime(this.props.info.creationTime), 'italics')));
	}
})

BranchManager.Components.Summary = ReactRedux.connect(state => ({ info: state.activeNode.info }))(BranchManager.Components.Summary);