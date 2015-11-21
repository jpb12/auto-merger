var TreeRoot = React.createClass({
	displayName: 'TreeRoot',
	render: function() {
		return (
			React.createElement(
				'svg',
				{
					height: '600px',
					width: '600px'
				},
				this.props.links.map(link => React.createElement(Link, { key: link.target.name, link: link, diagonal: this.props.diagonal })),
				this.props.nodes.map(node => React.createElement(Node, { key: node.name, node: node}))));
	}
});

var Node = React.createClass({
	displayName: 'Node',
	render: function() {
		return (
			React.createElement(
				'g',
				{
					transform: 'translate(' + (this.props.node.y + 50) + ', ' + (this.props.node.x + 50) + ')'
				},
				React.createElement(Circle),
				React.createElement(Text, {name: this.props.node.name})));
	}
});

var Circle = React.createClass({
	displayName: 'Circle',
	render: function() {
		return (
			React.createElement('circle'));
	}
})

var Text = React.createClass({
	displayName: 'Text',
	render: function() {
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

var Link = React.createClass({
	displayName: 'Link',
	render: function() {
		return (
			React.createElement(
				'path',
				{
					d: this.props.diagonal({ source: this.props.link.source, target: this.props.link.target }),
					transform: 'translate(50, 50)'
				}))
	}
});