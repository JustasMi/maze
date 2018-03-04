// Write more js ?
var canvas = $("#canvas")[0];;
var canvasContext = canvas.getContext("2d");
const direction = Object.freeze({
	"up": 1,
	"down": 2,
	"left": 3,
	"right": 4
});
var cellSize = 30;
var startingPosition = 5;
var cellData;

var playerX = 20;
var playerY = 20;

/*
function Cell(left, right, top, bottom) {
	this.left = left;
	this.right = right;
	this.top = top;
	this.bottom = bottom;
}
*/

$.getJSON("?handler=Maze", function (data) {
	cellData = JSON.parse(data).result.cells;
	drawScene();
});

function drawCell(x, y, cell) {	
	var left = cell.left;
	var right = cell.right;
	var top = cell.top;
	var bottom = cell.bottom;

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
		console.log("w");
		if (canMove(playerX, playerY - cellSize, direction.up)) {
			playerY -= cellSize;
		}
	}
	if (e.key === "a") {
		// a
		console.log("a");
		if (canMove(playerX - cellSize, playerY, direction.left)) {
			playerX -= cellSize
		}
	}
	if (e.key === "d") {
		// d
		console.log("d");
		if (canMove(playerX + cellSize, playerY, direction.right)) {
			playerX += cellSize
		}
	}
	if (e.key === "s") {
		// s
		console.log("s");
		if (canMove(playerX, playerY + cellSize, direction.down)) {
			playerY += cellSize;
		}
	}
	console.log("(" + playerX + ", " + playerY + ")");

	canvasContext.clearRect(0, 0, canvas.width, canvas.height);

	drawScene();
});

function canMove(targetX, targetY, directio) {
	let isValidMove = false;

	const currentX = Math.floor(playerX / cellSize);
	const currentY = Math.floor(playerY / cellSize);

	const destinationX = Math.floor(targetX / cellSize);
	const destinationY = Math.floor(targetY / cellSize);

	const boundaryX = cellData[0].length;
	const boundaryY = cellData.length;

	if (destinationX < 0 || destinationX >= boundaryX || destinationY < 0 || destinationY >= boundaryY) {
		return false;
	}

	const currentCell = cellData[currentY][currentX];
	const destinationCell = cellData[destinationY][destinationX];

	if (directio === direction.down) {
		isValidMove = !currentCell.bottom && !destinationCell.top;
	} else if (directio === direction.up) {
		isValidMove = !currentCell.top && !destinationCell.bottom;
	} else if (directio === direction.left) {
		isValidMove = !currentCell.left && !destinationCell.right;
	} else {
		isValidMove = !currentCell.right && !destinationCell.left;
	}

	return isValidMove;
}