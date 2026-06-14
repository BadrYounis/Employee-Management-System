const API = "https://localhost:7193/api/Employee";

let modal;

window.onload = () => {
  modal = new bootstrap.Modal(document.getElementById("employeeModal"));
  let today = new Date().toISOString().split("T")[0];

  document.getElementById("hireDate").setAttribute("max", today);

  loadEmployees();
};

// GET + SEARCH

async function loadEmployees() {
  let search = document.getElementById("searchInput").value;

  let url = API;

  if (search) {
    url += "?SearchValue=" + search;
  }

  let response = await fetch(url);

  let employees = await response.json();

  let rows = "";

  employees.forEach((e) => {
    rows += `
<tr>

<td>${e.fullName}</td>

<td>${e.email}</td>

<td>${e.phoneNumber}</td>

<td>${e.jobTitle}</td>

<td>${e.hireDate}</td>

<td>${e.departmentName}</td>


<td>

${
  e.isActive
    ? '<span class="badge bg-success">Active</span>'
    : '<span class="badge bg-danger">Inactive</span>'
}

</td>



<td>


<button 
class="btn btn-warning btn-sm"
onclick="editEmployee(${e.id})">

Edit

</button>



<button 
class="btn btn-danger btn-sm"
onclick="deleteEmployee(${e.id})">

Delete

</button>


<button
class="btn btn-secondary btn-sm"
onclick="toggleStatus(${e.id})">

Status

</button>


</td>



</tr>
`;
  });

  document.getElementById("employeeTable").innerHTML = rows;
}

function openAddModal() {
  document.getElementById("modalTitle").innerText = "Add Employee";

  document.getElementById("employeeId").value = "";

  document.getElementById("fullName").readOnly = false;

  document.getElementById("email").readOnly = false;

  document.getElementById("hireDate").readOnly = false;

  clearForm();
}

async function saveEmployee() {
  let id = document.getElementById("employeeId").value;

  let deptId = Number(document.getElementById("departmentId").value);

  if (deptId <= 0) {
    alert("Department ID must be greater than 0");

    return;
  }

  let employee;

  if (id) {
    employee = {
      phoneNumber: document.getElementById("phoneNumber").value,

      jobTitle: document.getElementById("jobTitle").value,

      departmentId: deptId,
    };

    response = await fetch(API + "/" + id, {
      method: "PUT",

      headers: {
        "Content-Type": "application/json",
      },

      body: JSON.stringify(employee),
    });
  }

  else {
    employee = {
      fullName: document.getElementById("fullName").value,

      email: document.getElementById("email").value,

      phoneNumber: document.getElementById("phoneNumber").value,

      jobTitle: document.getElementById("jobTitle").value,

      hireDate: document.getElementById("hireDate").value,

      departmentId: deptId,
    };

    response = await fetch(API, {
      method: "POST",

      headers: {
        "Content-Type": "application/json",
      },

      body: JSON.stringify(employee),
    });
  }

  if (response.ok) {
    modal.hide();

    await loadEmployees();

    alert("Saved Successfully");
  } else {
    let error = await response.text();

    alert(error);
  }
}

async function editEmployee(id) {
  let response = await fetch(API + "/" + id);

  let e = await response.json();

  document.getElementById("employeeId").value = e.id;

  document.getElementById("fullName").value = e.fullName;

  document.getElementById("email").value = e.email;

  document.getElementById("phoneNumber").value = e.phoneNumber;

  document.getElementById("jobTitle").value = e.jobTitle;

  document.getElementById("hireDate").value = e.hireDate;

  document.getElementById("departmentId").value = e.departmentId;

  document.getElementById("fullName").readOnly = true;

  document.getElementById("email").readOnly = true;

  document.getElementById("hireDate").readOnly = true;

  document.getElementById("modalTitle").innerText = "Edit Employee";

  modal.show();
}

async function deleteEmployee(id) {
  if (confirm("Delete employee?")) {
    await fetch(API + "/" + id, {
      method: "DELETE",
    });

    loadEmployees();
  }
}

async function toggleStatus(id) {
  await fetch(API + "/" + id + "/toggle-status", {
    method: "PUT",
  });

  loadEmployees();
}

function clearForm() {
  document.querySelectorAll(".modal input").forEach((x) => (x.value = ""));
}
