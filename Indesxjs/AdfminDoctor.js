document.getElementById("createDoctorForm").addEventListener("submit", function (event) {
            var doctorName = document.getElementById("Doctorname").value;
            var email = document.getElementById("Email").value;
            var phoneNumber = document.getElementById("Phonenumber").value;
            var password = document.getElementById("Password").value;
            var specialization = document.getElementById("Specialization").value;

            if (doctorName.trim() === "") {
                alert("Please enter a valid doctor name.");
                event.preventDefault(); // Prevent form submission
            }

            if (email.trim() === "") {
                alert("Please enter a valid email address.");
                event.preventDefault(); // Prevent form submission
            }

            if (phoneNumber.trim() === "") {
                alert("Please enter a valid phone number.");
                event.preventDefault(); // Prevent form submission
            }

            if (password.trim() === "") {
                alert("Please enter a password.");
                event.preventDefault(); // Prevent form submission
            }

            if (specialization === "") {
                alert("Please select a specialization.");
                event.preventDefault(); // Prevent form submission
            }

    if (doctorName.trim() !== "" && email.trim() !== "" && phoneNumber.trim() !== "" && password.trim() !== "" && specialization !== "") {
        event.preventDefault(); // Prevent form submission

       
        alert("Doctor created successfully!");

        
    }
        });