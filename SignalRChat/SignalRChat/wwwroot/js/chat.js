const conexion = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

// Recibimos el mensaje del hub
conexion.on("GetMessage", (data) => {
    const div = document.createElement("div");
    const imgAvatar = document.createElement("img");
    imgAvatar.src = data.avatar;
    imgAvatar.alt = "Avatar de " + data.user;
    div.appendChild(imgAvatar);
    const mensaje = document.createElement("span");
    mensaje.textContent = data.user + " - " + data.text;
    div.appendChild(mensaje);
    const li = document.createElement("li");
    li.appendChild(div);
    document.getElementById("lstMensajes").appendChild(li);
});

// Evento de conexión
//conexion.start().then(() => {
//    const li = document.createElement("li");
//    li.textContent = "Bienvenido al chat";
//    document.getElementById("lstMensajes").appendChild(li);
//}).catch((error) => {
//    alert("Error de conexión");
//    console.error(error);
//});

document.getElementById("txtUsuario").addEventListener("input", function (event) {
    document.getElementById("btnConectar").disabled =
        event.target.value === "" || document.getElementById("txtAvatar").value===""
});

document.getElementById("txtAvatar").addEventListener("input", function (event) {
    document.getElementById("btnConectar").disabled =
        event.target.value === "" || document.getElementById("txtUsuario").value === ""
});

document.getElementById("btnConectar").addEventListener("click", (event) => {
    if (conexion.state === signalR.HubConnectionState.Disconnected) {
        conexion.start().then(() => {
            const li = document.createElement("li");
            li.textContent = "Conectado con el servidor en tiempo real";
            document.getElementById("lstMensajes").appendChild(li);
            document.getElementById("btnConectar").textContent = "Desconectar";
            document.getElementById("txtUsuario").disabled = true;
            document.getElementById("txtAvatar").disabled = true;
            document.getElementById("sala").disabled = true;
            document.getElementById("txtMensaje").disabled = false;
            document.getElementById("btnEnviar").disabled = false;

            const usuario = document.getElementById("txtUsuario").value;
            const avatar = document.getElementById("txtAvatar").value;
            const sala = document.getElementById("sala").value;

            const message = {
                user: usuario,
                avatar: avatar,
                room:sala,
                text: ""
            }

            conexion.invoke("SendMessage", message).catch(function (error) {
                console.error(error);
            });

        }).catch(function (error) {
            console.error(error);
        });
    }
    else if (conexion.state === signalR.HubConnectionState.Connected) {
        conexion.stop();

        const li = document.createElement("li");
        li.textContent = "Has salido del chat";
        document.getElementById("lstMensajes").appendChild(li);
        document.getElementById("btnConectar").textContent = "Conectar";
        document.getElementById("txtUsuario").disabled = false;
        document.getElementById("txtAvatar").disabled = false;
        document.getElementById("sala").disabled = false;
        document.getElementById("txtMensaje").disabled = true;
        document.getElementById("btnEnviar").disabled = true;
    }
});

document.getElementById("btnEnviar").addEventListener("click", (event) => {
    const usuario = document.getElementById("txtUsuario").value;
    const mensaje = document.getElementById("txtMensaje").value;
    const avatar = document.getElementById("txtAvatar").value;
    const sala = document.getElementById("sala").value;

    const data = {
        user: usuario,
        text: mensaje,
        avatar: avatar,
        room:sala
    };

    // invoke nos va a comunicar con el hub y el evento para pasarle el mensaje
    conexion.invoke("SendMessage",data).catch((error) => {
        console.error(error);
    });
    document.getElementById("txtMensaje").value = "";
    event.preventDefault(); // Para evitar el submit
})
