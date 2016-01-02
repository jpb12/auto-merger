var Link = React.createClass({
	displayName: 'Link',
	render: function () {
		return (
			React.createElement(
				'path',
				{
					d: this.props.diagonal({ source: this.props.link.source, target: this.props.link.target }),
					transform: 'translate(' + this.props.margins.top + ', ' + this.props.margins.left + ')'
				}))
	}
});