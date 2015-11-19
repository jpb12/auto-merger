var TreeRoot = React.createClass({
	displayName: 'TreeRoot',
	render: function() {
		return (
			React.createElement(
				'svg',
				{
					className: "tree",
					height: '600px',
					width: '600px'
				},
				this.props.nodes.map(node => React.createElement(Node, { key: node.name, node: node}))));
	}
});

var Node = React.createClass({
	displayName: 'Node',
	render: function () {
		return (
			React.createElement(
				'g',
				{
					className: 'node',
					transform: 'translate(' + (this.props.node.x + 50) + ', ' + (this.props.node.y + 50) + ')'
				},
				React.createElement(Circle)));
	}
})

var Circle = React.createClass({
	displayName: 'Circle',
	render: function () {
		return (
			React.createElement('circle'));
	}
})