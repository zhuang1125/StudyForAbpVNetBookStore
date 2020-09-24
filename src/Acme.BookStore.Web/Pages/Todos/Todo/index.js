$(function () {

    var l = abp.localization.getResource('BookStore');

    var service = acme.bookStore.todos.todo;
    var createModal = new abp.ModalManager(abp.appPath + 'Todos/Todo/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Todos/Todo/EditModal');

    var dataTable = $('#TodoTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        autoWidth: false,
        scrollCollapse: true,
        order: [[0, "asc"]],
        ajax: abp.libs.datatables.createAjax(service.getList),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l('Edit'),
                                visible: abp.auth.isGranted('BookStore.Todo.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('BookStore.Todo.Delete'),
                                confirmMessage: function (data) {
                                    return l('TodoDeletionConfirmationMessage', data.record.id);
                                },
                                action: function (data) {
                                        service.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l('SuccessfullyDeleted'));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
            { data: "content" },
            { data: "dueTime" },
            { data: "isDone" },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewTodoButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});