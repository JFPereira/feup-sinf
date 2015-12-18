$(function () {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/clients",
        success: function (clients) {
            clients = JSON.parse(clients);

            console.log(clients);

            var dataS = [];
            $.each(clients, function (i) {
                var action = '<a id="client-button" href="/Clients/Show/' + clients[i].CodCliente + '" class="btn btn-success btn-block">Details</a>';
                if(clients[i].CodCliente != "VD")
                    dataS.push([clients[i].CodCliente, clients[i].NomeCliente, clients[i].Morada, clients[i].Fac_Tel, clients[i].NumContribuinte, action]);
            });

            $('#clients').dataTable({
                data: dataS,
                columns: [
                    { title: "Name" },
                    { title: "Fiscal Name" },
                    { title: "Adress" },
                    { title: "Phone Number" },
                    { title: "Tax Identification Number" },
                    { title: "Action" }
                ],
                destroy: true
            });

            $("#clientsListLoadingAnimation").remove();
        }
     })
});