<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>School Management System API Documentation</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f4f4f4;
        }
        .container {
            width: 80%;
            margin: auto;
            padding: 20px;
            background-color: #fff;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
        h1, h2, h3 {
            color: #333;
        }
        h1 {
            text-align: center;
        }
        ul {
            list-style-type: none;
            padding: 0;
        }
        li {
            padding: 5px 0;
        }
        .section-title {
            font-size: 1.2em;
            font-weight: bold;
            color: #0056b3;
            margin-top: 20px;
        }
        code {
            background-color: #eee;
            padding: 2px 4px;
            font-size: 0.95em;
            color: #d14;
        }
        .footer {
            text-align: center;
            padding: 10px;
            font-size: 0.9em;
            color: #666;
        }
    </style>
</head>
<body>
    <div class="container">
        <h1>📋 ASP.NET Core Web API for School Management System</h1>

        <p>This project is an <strong>ASP.NET Core Web API</strong> designed to manage a comprehensive school system. It organizes student, teacher, and guardian data, covering classes, buildings, rooms, campuses, sections, shifts, and curriculum management. This API supports CRUD operations and efficiently handles master-detail relationships for complex associations within the school system.</p>

        <h2>🌟 Features</h2>
        
        <div class="section-title">Campus & Building Management</div>
        <ul>
            <li><strong>Campus Management:</strong> Add, edit, view, and delete campus records, including campus name, location, and associated classes.</li>
            <li><strong>Building Management:</strong> Organize buildings within campuses and manage individual rooms for classroom assignments.</li>
        </ul>

        <div class="section-title">Class & Section Management</div>
        <ul>
            <li><strong>Class Management:</strong> Manage classes with curriculum, subjects, and shifts, allowing flexible organization by grade or program.</li>
            <li><strong>Section Management:</strong> Organize sections for each class with dynamic assignments for better student management.</li>
        </ul>

        <div class="section-title">Guardian & Student Management</div>
        <ul>
            <li><strong>Guardian Management:</strong> Manage guardians with contact information and relationship to students.</li>
            <li><strong>Student Management:</strong> Supports student profiles with guardians, assigned classes, sections, and profile images.</li>
        </ul>

        <div class="section-title">Curriculum & Subject Management</div>
        <ul>
            <li><strong>Curriculum Management:</strong> Define and manage the curriculum and associated subjects for each class.</li>
            <li><strong>Subject Management:</strong> Organize subjects by class and assign teachers for efficient subject planning.</li>
        </ul>

        <div class="section-title">Teacher & Shift Management</div>
        <ul>
            <li><strong>Teacher Management:</strong> Track teacher information, assigned subjects, and schedules.</li>
            <li><strong>Shift Management:</strong> Organize class shifts (morning, afternoon, evening) to optimize space and resources.</li>
        </ul>

        <h2>🛠 Technologies</h2>
        <ul>
            <li><strong>ASP.NET Core Web API:</strong> Framework for developing API endpoints and business logic.</li>
            <li><strong>Entity Framework Core:</strong> Manages database operations, relationships, and data integrity.</li>
            <li><strong>SQL Server:</strong> Backend for storing school-related data.</li>
            <li><strong>C#:</strong> Programming language for business logic.</li>
            <li><strong>HTML/CSS/JavaScript:</strong> Potential integration with any front-end framework.</li>
        </ul>

        <h2>🚀 API Endpoints</h2>
        
        <div class="section-title">Campus, Building & Room Endpoints</div>
        <ul>
            <li><code>GET /api/Campus</code>: Retrieve all campuses.</li>
            <li><code>POST /api/Campus</code>: Add a new campus.</li>
            <li><code>POST /api/Buildings</code>: Add or update a building within a campus.</li>
            <li><code>POST /api/BuildingRooms</code>: Manage rooms within each building.</li>
        </ul>

        <div class="section-title">Class, Section & Curriculum Endpoints</div>
        <ul>
            <li><code>GET /api/Classes</code>: Retrieve a list of all classes.</li>
            <li><code>POST /api/Classes</code>: Add or edit class information, including curriculum.</li>
            <li><code>POST /api/Sections</code>: Manage sections for each class, with options to assign shifts.</li>
        </ul>

        <div class="section-title">Student, Guardian & Teacher Endpoints</div>
        <ul>
            <li><code>GET /api/Students</code>: Retrieve all students with guardians and class information.</li>
            <li><code>POST /api/Students</code>: Add or edit student information, including profile image.</li>
            <li><code>POST /api/Guardians</code>: Manage guardian records and assign to students.</li>
            <li><code>POST /api/Teachers</code>: Add, edit, and view teacher details and assigned subjects.</li>
        </ul>

        <div class="section-title">Subject & Curriculum Endpoints</div>
        <ul>
            <li><code>GET /api/Subjects</code>: Retrieve all subjects offered.</li>
            <li><code>POST /api/Subjects</code>: Add or update subject information.</li>
            <li><code>POST /api/TeacherSubjects</code>: Assign teachers to subjects for streamlined management.</li>
        </ul>

        <p>This API is optimized for managing school data, making it ideal for integration into larger educational platforms or as a standalone solution for institutions of any size. By structuring data relationships clearly, it simplifies managing complex school associations.</p>
    </div>

    <div class="footer">
        <p>© 2024 School Management System API Documentation</p>
    </div>
</body>
</html>
