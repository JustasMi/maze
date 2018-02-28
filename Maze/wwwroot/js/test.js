// Write more js ?
var canvas = $("#canvas")[0];;
var canvasContext = canvas.getContext("2d");;
var cellSize = 30;
var startingPosition = 5;
var cellData;

var playerX = 20;
var playerY = 20;
var playerTravelDistance = cellSize;

/*
function Cell(left, right, top, bottom) {
	this.left = left;
	this.right = right;
	this.top = top;
	this.bottom = bottom;
}
*/

$.getJSON("?handler=Maze", function (data) {
	//Do something with the data.
	debugger;
	//var test = new Cell(true, false, true, false);
	//var obj = JSON.parse(data).result;
	cellData = JSON.parse(data).result.cells;
	drawScene();
	/*
	let x = startingPosition;
	let y = startingPosition;
	for (let i = 0; i < obj.cells.length; i++) {
		for (let j = 0; j < obj.cells[i].length; j++) {
			var value = obj.cells[i][j];
			console.log(value);
			drawCell(x, y, value);
			x += cellSize;
		}
		y += cellSize;
		x = startingPosition;
	}
	*/
});

function drawCell(x, y, cell) {
	/* Draw cell based on wall properties */
	
	var left = cell.left;
	var right = cell.right;
	var top = cell.top;
	var bottom = cell.bottom;
	
	/*
	var left = true;
	var right = true;
	var top = true;
	var bottom = false;
	*/
	var size = cellSize;
	canvasContext.beginPath();
	canvasContext.strokeStyle = "#000000";
	canvasContext.lineWidth = 2;
	if (left) {
		drawLine(x, y, x, y + size);
	}

	if (right) {
		drawLine(x + size, y, x + size, y + size);
	}

	if (bottom) {
		drawLine(x, y + size, x + size, y + size)
	}

	if (top) {
		drawLine(x, y, x + size, y);
	}
	canvasContext.stroke();
}

function drawLine(sX, sY, eX, eY) {
	/*Draw a line from the starting X and Y positions to  the ending X and Y positions*/
	canvasContext.moveTo(sX, sY);
	canvasContext.lineTo(eX, eY);
}

function drawScene() {
	let x = startingPosition;
	let y = startingPosition;
	canvasContext.beginPath();
	drawPlayer(playerX, playerY);
	for (let i = 0; i < cellData.length; i++) {
		for (let j = 0; j < cellData[i].length; j++) {
			var value = cellData[i][j];
			drawCell(x, y, value);
			x += cellSize;
		}
		y += cellSize;
		x = startingPosition;
	}
}

function drawPlayer(x, y) {
	canvasContext.arc(x, y, 5, 0, 2 * Math.PI, false);
	canvasContext.fillStyle = 'green';
	canvasContext.fill();
	canvasContext.lineWidth = 5;
	canvasContext.strokeStyle = '#003300';
	canvasContext.stroke();
}

$(document).keypress(function (e) {
	if (e.key === "w") {
		// w
		playerY -= playerTravelDistance;
		console.log("w");
	}
	if (e.key === "a") {
		// a
		console.log("a");
		playerX -= playerTravelDistance
	}
	if (e.key === "d") {
		// d
		console.log("d");
		playerX += playerTravelDistance
	}
	if (e.key === "s") {
		// s
		console.log("s");
		playerY += playerTravelDistance;
	}
	console.log("(" + playerX + ", " + playerY + ")");
	canvasContext.clearRect(0, 0, canvas.width, canvas.height);

	drawScene();
});