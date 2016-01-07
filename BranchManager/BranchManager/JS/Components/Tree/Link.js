BranchManager.Components.Link = React.createClass({
	branch: function() {
		return this.props.link.source.branches.filter(branch => branch.child == this.props.link.target)[0];
	},
	displayName: 'Link',
	getClassName: function () {
		var className = this.branch().enabled ? 'enabled' : 'disabled'
		className += this.props.link.source.exists && this.props.link.target.exists
			? ' exits'
			: ' not-exists';
		return className;
	},
	getColour: function() {
		var colours = [
			{ r: 42, g: 243, b: 39, percent: 0 },
			{ r: 2, g: 187, b: 170, percent: 0.25 },
			{ r: 239, g: 0, b: 255, percent: 0.35 },
			{ r: 210, g: 0, b: 145, percent: 0.55 },
			{ r: 173, g: 0, b: 0, percent: 1 }
		];

		var percent = Math.min((this.branch().unmergedRevisions / this.props.commits), 1);

		for (var i = 0; i < colours.length; i++) {
			var firstColour = colours[i];
			var secondColour = colours[i + 1];

			if (secondColour.percent < percent) {
				continue;
			}

			var percentAlong = (percent - firstColour.percent) / (secondColour.percent - firstColour.percent);

			var red = Math.round(firstColour.r + (secondColour.r - firstColour.r) * percentAlong);
			var blue = Math.round(firstColour.b + (secondColour.b - firstColour.b) * percentAlong);
			var green = Math.round(firstColour.g + (secondColour.g - firstColour.g) * percentAlong);

			return 'rgb(' + red + ', ' + blue + ', ' + green + ')';
		}
	},
	getD: function() {
		var diagonal = this.props.horizontal
			? d3.svg.diagonal().projection(d => [d.y, d.x])
			: d3.svg.diagonal().projection(d => [d.x, d.y]);

		return diagonal({ source: this.props.link.source, target: this.props.link.target });
	},
	render: function() {
		return (
			React.createElement(
				'path',
				{
					className: this.getClassName(),
					stroke: this.getColour(),
					d: this.getD()
				}))
	}
});

BranchManager.Components.Link = ReactRedux.connect(
	state => (
		{
			commits: state.settings.commits,
			horizontal: state.settings.horizontal
	}))(BranchManager.Components.Link);