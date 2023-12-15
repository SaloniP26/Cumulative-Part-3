// AJAX for teacher Add can go in here!
// This file is connected to the project via Shared/_Layout.cshtml

function AddTeacher() {

	//goal: send a request which looks like this:
	//POST : http://localhost:51326/api/TeacherData/AddTeacher
	//with POST data of Name, Lastname, Employee number, hire date, salary , etc.

	var URL = "http://localhost:51326/api/TeacherData/AddTeacher/";

	var rq = new XMLHttpRequest();
	// where is this request sent to?
	// is the method GET or POST?
	// what should we do with the response?

	var Name = document.getElementById('Name').value;
	var LastName = document.getElementById('LastName').value;
	var EmpNum = document.getElementById('EmpNum').value;
	var HireDate = document.getElementById('HireDate').value;
	var Salary = document.getElementById('Salary').value;


	var TeacherData = {
		"Name": Name,
		"LastName": LastName,
		"EmpNum": EmpNum,
		"HireDate": HireDate,
		"Salary": Salary
	};


	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished

			//nothing to render, the method returns nothing.


		}

	}
	//POST information sent through the .send() method
	rq.send(JSON.stringify(TeacherData));

}



function UpdateTeacher(Id) {

	//goal: send a request which looks like this:
	//POST : http://localhost:51326/api/TeacherData/UpdateTeacher/{id}
	//with POST data of Name, Lastname, Employee number, hire date, salary, etc.

	var URL = "http://localhost:51326/api/TeacherData/UpdateTeacher/" + Id;

	var rq = new XMLHttpRequest();
	// where is this request sent to?
	// is the method GET or POST?
	// what should we do with the response?

	var Name = document.getElementById('Name').value;
	var LastName = document.getElementById('LastName').value;
	var EmpNum = document.getElementById('EmpNum').value;
	var HireDate = document.getElementById('HireDate').value;
	var Salary = document.getElementById('Salary').value;



	var TeacherData = {
		"Name": Name,
		"LastName": LastName,
		"EmpNum": EmpNum,
		"HireDate": HireDate,
		"Salary": Salary
	};


	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished

			//nothing to render, the method returns nothing.


		}

	}
	//POST information sent through the .send() method
	rq.send(JSON.stringify(TeacherData));

}