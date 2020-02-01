function agendarHorario() {
    $("#modalAgendar").modal("show");
    return false;
}

$(".btnAtualizar").click(function () {
    var id = $(this).data("value");
    $("#formAtualizar").load("/AgendaAdm/AtualizarDadosClientes/" + id,
        function () {
            $("#modalAtualizar").modal("show");
            return false;
        });
});

$(".btnDeletar").click(function () {
    var id = $(this).data("value");
    $("#formDeletar").load("/AgendaAdm/DeletarAgendamento/" + id,
        function () {
            $("#modalDeletar").modal("show");
            return false;
        });
});

$("#agendarForm").submit(function (event) {
    var data = $("#agendarForm").serialize();

    $.ajax({
        type: 'POST',
        url: '/AgendaAdm/AgendarClienteAdm',
        data: data,
        dataType: "JSON",
        success: function (response) {
            if (response == "sucesso") {
                window.location.reload();
            }
            else if (response == "nome incompleto") {
                $("#abrirModalResposta").click();
                $(".informacao").text("Insira seu nome corretamente !!!");
            }
            else if (response == "nome invalido") {
                $("#abrirModalResposta").click();
                $(".informacao").text("Este nome já foi cadastrado, insira outro !!!");
            }
            else if (response == "data indisponivel") {
                $("#abrirModalResposta").click();
                $(".informacao").text("Esta Data Hora já foi escolhida por outro cliente !!!");
            }
            else if (response == "hora invalida") {
                $("#abrirModalResposta").click();
                $(".informacao").text("Este horário não estamos em funcionamento !!!");
            }
            else if (response == "dia anterior") {
                $("#abrirModalResposta").click();
                $(".informacao").text("Escolha um horário de hoje em diante !!!");
            }
        }
    });
    return false;
});