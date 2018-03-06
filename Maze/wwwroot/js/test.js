// Write more js ?
var canvas = $("#canvas")[0];
var canvasContext = canvas.getContext("2d");
var maze;
var inputEnabled = false;

const direction = Object.freeze({
	"up": 1,
	"down": 2,
	"left": 3,
	"right": 4
});
/*
var cellSize = 30;
var startingPosition = 1;
var cellData;

var playerX = 20;
var playerY = 20;
*/

function Cell(left, right, top, bottom, goal) {
	this.left = left;
	this.right = right;
	this.top = top;
	this.bottom = bottom;
	this.goal = goal;

	this.cellStyle = "#000000";
	this.cellLineWidth = 2;
	this.cellSize = 30; // TODO: this is duplicated in maze object

	this.draw = function (x, y) {
		canvasContext.beginPath();
		canvasContext.strokeStyle = this.cellStyle;
		canvasContext.lineWidth = this.cellLineWidth;

		if (this.left) {
			this.drawLine(x, y, x, y + this.cellSize);
		}

		if (this.right) {
			this.drawLine(x + this.cellSize, y, x + this.cellSize, y + this.cellSize);
		}

		if (this.bottom) {
			this.drawLine(x, y + this.cellSize, x + this.cellSize, y + this.cellSize)
		}

		if (this.top) {
			this.drawLine(x, y, x + this.cellSize, y);
		}
		canvasContext.stroke();
	}

	this.drawLine = function (startX, startY, endX, endY) {
		canvasContext.moveTo(startX, startY);
		canvasContext.lineTo(endX, endY);
	}
}

function Maze(cells, configuration) {
	this.cells = cells;
	this.configuration = configuration;

	this.cellSize = 30;
	this.startingPosition = 2; // rename to maze origin
	this.player = new Player(this.cellSize);

	this.draw = function () {
		canvasContext.clearRect(0, 0, canvas.width, canvas.height);
		this.player.draw();
		let x = this.startingPosition;
		let y = this.startingPosition;
		for (let i = 0; i < this.cells.length; i++) {
			for (let j = 0; j < this.cells[i].length; j++) {
				this.cells[i][j].draw(x, y);
				x += this.cellSize;
			}
			y += this.cellSize;
			x = this.startingPosition;
		}
	};

	this.movePlayer = function (moveDirection) {
		if (moveDirection === direction.up) {
			if (this.canMove(this.player.playerX, this.player.playerY - this.cellSize, direction.up)) {
				this.player.playerY -= this.cellSize;
			}
		}

		if (moveDirection === direction.left) {
			if (this.canMove(this.player.playerX - this.cellSize, this.player.playerY, direction.left)) {
				this.player.playerX -= this.cellSize;
			}
		}

		if (moveDirection === direction.right) {
			if (this.canMove(this.player.playerX + this.cellSize, this.player.playerY, direction.right)) {
				this.player.playerX += this.cellSize;
			}
		}

		if (moveDirection === direction.down) {
			if (this.canMove(this.player.playerX, this.player.playerY + this.cellSize, direction.down)) {
				this.player.playerY += this.cellSize;
			}
		}
	};

	this.canMove = function (targetX, targetY, moveDirection) {
		let isValidMove = false;

		const currentX = Math.floor(this.player.playerX / this.cellSize);
		const currentY = Math.floor(this.player.playerY / this.cellSize);

		const destinationX = Math.floor(targetX / this.cellSize);
		const destinationY = Math.floor(targetY / this.cellSize);

		const boundaryX = this.cells[0].length;
		const boundaryY = this.cells.length;

		if (destinationX < 0 || destinationX >= boundaryX || destinationY < 0 || destinationY >= boundaryY) {
			return false;
		}

		const currentCell = this.cells[currentY][currentX];
		const destinationCell = this.cells[destinationY][destinationX];

		if (moveDirection === direction.down) {
			isValidMove = !currentCell.bottom && !destinationCell.top;
		} else if (moveDirection === direction.up) {
			isValidMove = !currentCell.top && !destinationCell.bottom;
		} else if (moveDirection === direction.left) {
			isValidMove = !currentCell.left && !destinationCell.right;
		} else {
			isValidMove = !currentCell.right && !destinationCell.left;
		}
		return isValidMove;
	};

	this.moveThroughSolved = async function (solution) {
		this.player = new Player(this.cellSize);
		inputEnabled = false;

		for (let i = 0; i < solution.length; i++) {
			this.player.playerX = solution[i].x * this.cellSize + 20;
			this.player.playerY = solution[i].y * this.cellSize + 20;
			this.draw();
			await sleep(500);
		}
	}
};

function sleep(ms) {
	return new Promise(resolve => setTimeout(resolve, ms));
}

function Player(cellSize) {
	this.playerX = 20;
	this.playerY = 20;
	this.cellSize = cellSize;

	this.draw = function () {
		canvasContext.beginPath();
		canvasContext.arc(this.playerX, this.playerY, 5, 0, 2 * Math.PI, false);
		canvasContext.fillStyle = "green";
		canvasContext.fill();
		canvasContext.lineWidth = 5;
		canvasContext.strokeStyle = "#003300";
		canvasContext.stroke();
	};
}

$.getJSON("?handler=Maze", function (data) {
	//cellData = JSON.parse(data).result.cells;
	//drawScene();
	const parsedData = JSON.parse(data).result;
	var mappedCells = Object.keys(parsedData.cells).map(key => {
		return parsedData.cells[key].map(cell => new Cell(cell.left, cell.right, cell.top, cell.bottom, cell.goal));
	});
	maze = new Maze(mappedCells, parsedData.configuration);
	maze.draw();
	inputEnabled = true;
});
/*
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
*/
$(document).keypress(function (event) {
	if (!inputEnabled) {
		return;
	}
	if (event.key === "w") {
		maze.movePlayer(direction.up);
	}
	if (event.key === "a") {
		maze.movePlayer(direction.left);
	}
	if (event.key === "d") {
		maze.movePlayer(direction.right);
	}
	if (event.key === "s") {
		maze.movePlayer(direction.down);
	}
	//canvasContext.clearRect(0, 0, canvas.width, canvas.height);
	maze.draw();
});

/*
$(document).keypress(function (event) {
	debugger;
	if (event.key === "w") {
		if (canMove(playerX, playerY - cellSize, direction.up)) {
			playerY -= cellSize;
		}
	}
	if (event.key === "a") {
		if (canMove(playerX - cellSize, playerY, direction.left)) {
			playerX -= cellSize
		}
	}
	if (event.key === "d") {
		if (canMove(playerX + cellSize, playerY, direction.right)) {
			playerX += cellSize
		}
	}
	if (event.key === "s") {
		if (canMove(playerX, playerY + cellSize, direction.down)) {
			playerY += cellSize;
		}
	}
	console.log("(" + playerX + ", " + playerY + ")");
	canvasContext.clearRect(0, 0, canvas.width, canvas.height);
	drawScene();
});
*/
/*
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
*/
function solveMaze() {
	const cells = $.extend(true, [], maze.cells);
	const queue = [];

	const mazeHeight = cells.length;
	const mazeWidth = cells[0].length;

	for (let i = 0; i < mazeHeight; i++) {
		for (let j = 0; j < mazeWidth; j++) {
			cells[i][j].x = j;
			cells[i][j].y = i;
		}
	}

	cells[0][0].label = 1;
	queue.push(cells[0][0]);

	while (queue.length > 0) {
		let currentCell = queue.pop();
		// check top
		if (isWithinBounds(currentCell.x, currentCell.y - 1, mazeWidth, mazeHeight)) {
			let neighbourCell = cells[currentCell.y - 1][currentCell.x];
			if (!neighbourCell.bottom && !currentCell.top && !neighbourCell.label) {
				neighbourCell.label = currentCell.label + 1;
				queue.push(neighbourCell);
			}
		}

		//check bottom
		if (isWithinBounds(currentCell.x, currentCell.y + 1, mazeWidth, mazeHeight)) {
			let neighbourCell = cells[currentCell.y + 1][currentCell.x];
			if (!neighbourCell.top && !currentCell.bottom && !neighbourCell.label) {
				neighbourCell.label = currentCell.label + 1;
				queue.push(neighbourCell);
			}
		}
		//check left
		if (isWithinBounds(currentCell.x - 1, currentCell.y, mazeWidth, mazeHeight)) {
			let neighbourCell = cells[currentCell.y][currentCell.x - 1];
			if (!neighbourCell.right && !currentCell.left && !neighbourCell.label) {
				neighbourCell.label = currentCell.label + 1;
				queue.push(neighbourCell);
			}
		}
		// check right
		if (isWithinBounds(currentCell.x + 1, currentCell.y, mazeWidth, mazeHeight)) {
			let neighbourCell = cells[currentCell.y][currentCell.x + 1];
			if (!neighbourCell.left && !currentCell.right && !neighbourCell.label) {
				neighbourCell.label = currentCell.label + 1;
				queue.push(neighbourCell);
			}
		}
	}
	debugger;
	let solution = [];
	let currentCell = cells[mazeHeight - 1][mazeWidth - 1];
	solution.push(currentCell);

	while (currentCell.label != 1) {
		// check top
		if (isWithinBounds(currentCell.x, currentCell.y - 1, mazeWidth, mazeHeight)) {
			let neighbourCell = cells[currentCell.y - 1][currentCell.x];
			if (neighbourCell.label === currentCell.label - 1) {
				solution.unshift(neighbourCell);
				currentCell = neighbourCell;
			}
		}

		//check bottom
		if (isWithinBounds(currentCell.x, currentCell.y + 1, mazeWidth, mazeHeight)) {
			let neighbourCell = cells[currentCell.y + 1][currentCell.x];
			if (neighbourCell.label === currentCell.label - 1) {
				solution.unshift(neighbourCell);
				currentCell = neighbourCell;
			}
		}

		//check left
		if (isWithinBounds(currentCell.x - 1, currentCell.y, mazeWidth, mazeHeight)) {
			let neighbourCell = cells[currentCell.y][currentCell.x - 1];
			if (neighbourCell.label === currentCell.label - 1) {
				solution.unshift(neighbourCell);
				currentCell = neighbourCell;
			}
		}

		// check right
		if (isWithinBounds(currentCell.x + 1, currentCell.y, mazeWidth, mazeHeight)) {
			let neighbourCell = cells[currentCell.y][currentCell.x + 1];
			if (neighbourCell.label === currentCell.label - 1) {
				solution.unshift(neighbourCell);
				currentCell = neighbourCell;
			}
		}
	}

	maze.moveThroughSolved(solution);
}

function isWithinBounds(x, y, maximumX, maximumY) {
	return x >= 0 && x < maximumX && y >= 0 && y < maximumY;
}