<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebDemoControles._Default" %>


<asp:Content ID="HeaderContent" ContentPlaceHolderID="ContentHeader" runat="server">

        <link href="Scripts/Common/Dx/Content/dx.common.css" rel="stylesheet" />
        <link href="Scripts/Common/Dx/Content/dx.light.css" rel="stylesheet" />
 

</asp:Content>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
   <div id="myForm"></div>
    
    <div id="txtFecha"></div>

    <input type="button" value="Guardar" id="btnGuardar"/>

</asp:Content>


<asp:Content ID="FooterContent" ContentPlaceHolderID="ContentFooter" runat="server">
    <script src="Scripts/Common/Dx/Scripts/globalize.min.js"></script>
    <script src="Scripts/Common/Dx/Scripts/dx.all.min.js"></script>
    <script src="Scripts/Common/Nz/nzControl.js"></script>
    <script src="Scripts/Common/Nz/nzControl.devextreme.js"></script>

     <script>
         (function () {

             $(document).on("click", "#btnGuardar", function (event) {

                 var data = $('#myForm').dxForm('instance').option('formData');
                 console.log(data);

            
             });

             $('#myForm').nzControl({
                     Action: 'http://localhost:64632/Api/Controles',
                     UUID: 'cd931fc1-9cab-49ea-8ef1-700d5db48d65'

             });


             //$(".numerico").dxNumberBox({
             //    //max: totalproductQuantity,
             //    //min: 0,
             //    value: 16,
             //    showSpinButtons: true,
             //    onKeyPress: function (e) {
             //        var event = e.jQueryEvent,
             //            str = String.fromCharCode(event.keyCode);
             //        if (!/[0-9]/.test(str))
             //            event.preventDefault();
             //    },
             //    onValueChanged: function (data) {
             //        productInventory.option("value", totalproductQuantity - data.value);
             //    }
             //});

         //    var varJson = '{"Nombre": "John","Apellido": "Heart","Puesto": "CEO","FechaNacimiento": "06/03/1964","Ciudad": "Los Angeles","Telefono": "+1(213) 555-9392","Email": "jheart@dx-email.com","Activo": "true"}';

         //    var employeeData = JSON.parse(varJson);

         //    var formOptions = {
         //        formData: employeeData,
         //        colCount: 3,
         //        items: [
         //                'Nombre',
         //                'Apellido',
         //                'Puesto',
         //                {
         //                    label: 'Fecha de Nacimiento',
         //                    dataField: 'FechaNacimiento',
         //                    editorType: 'dxDateBox'
         //                },
         //                'Ciudad',
         //                {
         //                    itemType: 'empty'
         //                },
         //                'Telefono',
         //                'Email', {

         //                    dataField: 'Activo',
         //                    editorType: 'dxSwitch'
         //                },
         //                 {
         //                     dataField: 'Estado',
         //                     editorType: 'dxSelectBox'
         //                 }
         //        ],

         //        customizeItem: function (item) {
         //            if (item.dataField === "Nombre" || item.dataField === "Apellido") {
         //                item.validationRules = [
         //                    {
         //                        type: "required",
         //                        message: "El valor es requerido"
         //                    },
         //                    {
         //                        type: "pattern",
         //                        pattern: "^[a-zA-Z]+$",
         //                        message: "El campo no debe de contener numeros"
         //                    }
         //                ]
         //            }
         //        }

         //    };

         //    $("#myForm").dxForm(
         //        formOptions
         //    );


         //    $(document).on('click', "#btnEnviar", function (event) {

         //        var data = $('#myForm').dxForm('instance').option('formData');
             //    });



             //customizeItem: function (item) {
             //    if (item.dataField !== 'Italics')
             //        return;
             //    item.template = function (data, itemElement) {
             //        var value = data.editorOptions.value;
             //        $('<input>').attr('type', 'checkbox').prop('checked', value).appendTo(itemElement);
             //    }
             //}
         })();

    </script>
 

</asp:Content>
