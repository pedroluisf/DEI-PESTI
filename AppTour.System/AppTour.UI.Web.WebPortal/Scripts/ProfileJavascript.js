 $(function () {
        
        $("#tabs").tabs();
        $("#tabs").tabs({ fx: { opacity: 'toggle'} });
        $("a", "#profile").button();
        $("a", "#profile").click(function () {
            if (this.id == "editProfile") {
                $("#dialog-form").dialog("open");
            }
        });
    
var name = $("#name"),
			email = $("#email"),
			password = $("#oldpassword"),
			allFields = $([]).add(name).add(email).add(password),
			tips = $(".validateTips");

		function updateTips(t) {
			tips
				.text(t)
				.addClass("ui-state-highlight");
			setTimeout(function () {
				tips.removeClass("ui-state-highlight", 1500);
			}, 500);
		}

		function checkLength(o, n, min, max) {
			if (o.val().length > max || o.val().length < min) {
				o.addClass("ui-state-error");
				updateTips("O tamanho de " + n + " tem que ser entre " + min + " e " + max + ".");
				return false;
			} else {
				return true;
			}
		}

		function checkRegexp(o, regexp, n) {
			if (!(regexp.test(o.val()))) {
				o.addClass("ui-state-error");
				updateTips(n);
				return false;
			} else {
				return true;
			}
		}
		$("#dialog:ui-dialog").dialog("destroy");
		$("#dialog-form").dialog({
			autoOpen: false,
			height: 520,
			width: 350,
			modal: true,
			buttons: {
				"Editar Perfil": function () {
					var bValid = true;
					allFields.removeClass("ui-state-error");
					
					var regexp = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

					bValid = bValid && checkLength(name, "name", 3, 80);
					bValid = bValid && checkRegexp(email, regexp, "Endereço de Email inválido!");
					bValid = bValid && checkLength(password, "oldpassword", 5, 80);

					if (bValid) {
						var data = jQuery('#form-id').serialize();
						$.ajax(
						{
							url: "@Url.Content("~/Account/UpdateProfile")",
							secureuri: false,
							data: data,
							dataType: 'text',
							success: function (data, status) {
								if (status == 'success') {
											 updateTips(data);
											}
							},
							error: function (data, status, e) {
								alert(e);
							}
			
					  });

					}
				},
				Cancel: function () {
					$(this).dialog("close");
				}
			},
			close: function () {
				allFields.val("").removeClass("ui-state-error");
			}
		});
        });