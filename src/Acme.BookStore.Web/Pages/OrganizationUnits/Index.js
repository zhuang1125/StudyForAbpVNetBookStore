$(function () {
    var l = abp.localization.getResource('BookStore');
   

    var dataTable = $('#OrganizationUnitsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,                       
            ajax: abp.libs.datatables.createAjax(acme.bookStore.organizationUnits.organizationUnit.getList),          
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted('BookStore.OrganizationUnits.Edit'), //CHECK for the PERMISSION
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('BookStore.OrganizationUnits.Delete'),
                                    confirmMessage: function (data) {
                                        return l('BookDeletionConfirmationMessage', data.record.name);
                                    },
                                    action: function (data) {
                                        acme.bookStore.organizationUnits.organizationUnit
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(l('SuccessfullyDeleted'));
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },            
                {
                    title: l('Code'),

                    data: "code"
                },  
                 {
                     title: l('DisplayName'),
                     data: "displayName"
                } 
            ]
        })
    );



    var createModal = new abp.ModalManager(abp.appPath + 'OrganizationUnits/CreateModal');

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });
    
    var editModal = new abp.ModalManager(abp.appPath + 'OrganizationUnits/EditModal');
    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewBookButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });



    var $table = $('#OrganizationUnitsTableTree')
    //自定义ajax方法
    function ajaxRequestGree(serverMethod, inputAction) {
        var input = inputAction ? inputAction(requestData, settings) : {};
        serverMethod(input).then(function (result) {
            result.total = result.totalCount;
            result.array = result.items;
            $table.bootstrapTable('load', result.items);
        });
    }




   

   
        $table.bootstrapTable({           
            idField: 'id',
            //showColumns: true,
            ajax: ajaxRequestGree(acme.bookStore.organizationUnits.organizationUnit.getList),
            columns: [
                {
                    field: 'ck',
                    checkbox: true
                },
                //{
                //    field: 'code',
                //    title: l('Code')
                //},
                {
                    field: 'displayName',
                    title: l('DisplayName')
                }
            ],
            treeShowField: 'displayName',
            parentIdField: 'parentId',
            onPostBody: function () {
                var columns = $table.bootstrapTable('getOptions').columns

                if (columns && columns[0][1].visible) {
                    $table.treegrid({
                        treeColumn: 1,
                        onChange: function () {
                            $table.bootstrapTable('resetView')
                        }
                    })
                }
            }
        })
   

    
});