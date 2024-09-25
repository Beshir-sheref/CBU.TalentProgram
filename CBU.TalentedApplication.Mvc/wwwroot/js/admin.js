$(document).ready(function () {
    // Handle nav-link clicks
    $('.nav-link').on('click', function (e) {
        e.preventDefault();

        var actionName = $(this).data('action-name');

        loadContent(actionName, 'main-content')

    });
});

function loadContent(actionName, divId) {
    fetch(`/Admin/${actionName}`)
        .then(response => response.text())
        .then(html => {
            document.getElementById(divId).innerHTML = html;

            runScriptsInDiv(document.getElementById(divId));

            intitializeForm();
        })
        .catch(error => console.error('Error loading content:', error, actionName));
}

function runScriptsInDiv(container) {
    const scripts = container.getElementsByTagName('script');
    for (let i = 0; i < scripts.length; i++) {
        eval(scripts[i].innerHTML); // Script'leri çalıştır
    }
}

//eventler listesi, daha önce tetiklenen eventin tekrar edilmesini engeller *yoksa ekle mantığı*
var initForms = [];

function intitializeForm() {

    function initForm(formId, success) {
        if (initForms.indexOf(formId) < 0)/*not existed*/ {

            const form = document.getElementById(formId);

            if (form) {
                form.addEventListener('submit', function (e) {
                    initForms.push(formId);

                    e.preventDefault();

                    const formData = new FormData(form);

                    fetch(form.action, {
                        method: 'POST',
                        body: formData,
                    }).then(response => response.json())
                        .then(data => {
                            if (data.success) {
                                success();
                            } else {
                                alert("Branch creation failed. Please try again.");
                            }
                        })
                        .catch(error => console.error('Error:', error));
                });
            }
        }
    }

    initForm("branchForm", function () {
        loadContent('GetBranches', 'table-container');
        loadContent('AddBranch', 'page-content');
    });

    //if (initForms.indexOf("branchForm") < 0) {

    //    const branchForm = document.getElementById('branchForm');
    //    if (branchForm) {
    //        branchForm.addEventListener('submit', function (e) {

    //            initForms.push("branchForm");

    //            e.preventDefault();

    //            const formData = new FormData(branchForm);

    //            fetch(branchForm.action, {
    //                method: 'POST',
    //                body: formData,
    //            }).then(response => response.json())
    //                .then(data => {
    //                    if (data.success) {
    //                        loadContent('GetBranches', 'table-container');
    //                        loadContent('AddBranch', 'page-content');
    //                    } else {
    //                        alert("Branch creation failed. Please try again.");
    //                    }
    //                })
    //                .catch(error => console.error('Error:', error));
    //        });
    //    }
    //}
}