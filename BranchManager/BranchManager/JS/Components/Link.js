﻿var Link = React.createClass({
	branch: function() {
		return this.props.link.source.branches.filter(branch => branch.child == this.props.link.target)[0];
	},
	displayName: 'Link',
	render: function () {
		return (
			React.createElement(
				'path',
				{
					className: this.branch().enabled ? 'enabled' : 'disabled',
					d: this.props.diagonal({ source: this.props.link.source, target: this.props.link.target }),
					transform: 'translate(' + this.props.margins.top + ', ' + this.props.margins.left + ')'
				}))
	}
});