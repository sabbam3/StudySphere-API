# StudySphere API
Overview
The Educational System API provides functionalities for managing subjects, grades, and user roles in an educational environment. The system includes roles for administrators, students, and lecturers, each with specific access rights.

Roles and Email Validation
Admin Role:

Email Format: something@admin.com
Responsibilities: Admins can add subjects, assign subjects to students.
Lecturer Role:

Email Format: something@lecturer.com
Responsibilities: Lecturers can add scores for students and view student grades.
Student Role:

Email Format: something(university admission year)@student.com
Responsibilities: Students can choose subjects, view grades, and see their chosen subjects.
Endpoints
1. Get Subjects
Endpoint: /subjects
Description: Retrieve a list of available subjects for students.
2. Get Chosen Subjects by Student
Endpoint: /students/{studentId}/subjects
Description: Retrieve the subjects chosen by a specific student.
3. Choose a Subject
Endpoint: /students/{studentId}/choose-subject
Description: Allow a student to choose a subject.
4. Add Score for a Student
Endpoint: /grades/{studentId}/add-score
Description: Allow a lecturer to add a score for a student.
5. Add a New Subject
Endpoint: /admin/add-subject
Description: Allow an admin to add a new subject.
