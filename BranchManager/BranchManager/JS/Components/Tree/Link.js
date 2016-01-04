var Link = React.createClass({
	branch: function() {
		return this.props.link.source.branches.filter(branch => branch.child == this.props.link.target)[0];
	},
	displayName: 'Link',
	diagonal: d3.svg.diagonal().projection(function (d) { return [d.y, d.x]; }),
	render: function () {
		return (
			React.createElement(
				'path',
				{
					className: this.branch().enabled ? 'enabled' : 'disabled',
					d: this.diagonal({ source: this.props.link.source, target: this.props.link.target })
				}))
	}
});