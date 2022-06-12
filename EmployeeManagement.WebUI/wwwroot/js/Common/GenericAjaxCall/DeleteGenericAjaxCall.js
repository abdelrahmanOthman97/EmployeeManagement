function DeleteRequest(url) {
    //console.log("# ----------- start of ajax calling -------------");
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                method: "DELETE",
                xhrFields: {
                    withCredentials: true
                },
                dataType: 'text',
                success: function (response) {
                    var responseJson = JSON.parse(response);
                    //console.log(responseJson);

                    if (responseJson.isSucceeded === true) {
                        Swal.fire(
                            'Deleted',
                            'Deleted Successfully.',
                            'success'
                        ).then(function () {
                            location.reload(true);
                        });
                    } else if (responseJson.isSucceeded === false) {
                        Swal.fire(
                            'Error!',
                            responseJson.errorMsg,
                            'error'
                        );
                    } else {
                        Swal.fire(
                            'Deleted',
                            'Deleted Successfully.',
                            'success'
                        ).then(function () {
                            location.reload(true);
                        });
                    }
                },
                error: function (response, res) {
                    Swal.fire(
                        'Error!',
                        'Your record could not  be deleted.',
                        'error'
                    );
                }
            });
        }
    });

    // console.log("# ----------- end of ajax calling -------------");
}



