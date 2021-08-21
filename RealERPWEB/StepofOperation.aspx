<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="StepofOperation.aspx.cs" Inherits="RealERPWEB.StepofOperation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        #accordion-style-1 h1,
        #accordion-style-1 a {
            color: #007b5e;
        }

        #accordion-style-1 .btn-link {
            font-weight: 400;
            color: #007b5e;
            background-color: transparent;
            text-decoration: none !important;
            font-size: 16px;
            font-weight: bold;
            padding-left: 25px;
        }

        #accordion-style-1 .card-body {
            border-top: 2px solid #007b5e;
        }

        #accordion-style-1 .card-header .btn.collapsed .fa.main {
            display: none;
        }

        #accordion-style-1 .card-header .btn .fa.main {
            background: #007b5e;
            padding: 13px 11px;
            color: #ffffff;
            width: 35px;
            height: 41px;
            position: absolute;
            left: -1px;
            top: 10px;
            border-top-right-radius: 7px;
            border-bottom-right-radius: 7px;
            display: block;
        }

        .btn-block {
            background-color: #dff0d8 !important;
            border: none;
            border-bottom: 1px solid #C9C9C9;
        }

        .card-body ul, #repul {
            list-style-type: none;
            color: #000000;
        }

            .card-body ul li, #repul li {
                padding: 2px 0px;
            }

                .card-body ul li a, #repul li a {
                    font-size: 14px;
                    color: #000000;
                }

             .card-body ul, #repulMid {
            list-style-type: none;
            color: #000000;
        }

            .card-body ul li, #repulMid li {
                padding: 2px 0px;
            }

                .card-body ul li a, #repulMid li a {
                    font-size: 14px;
                    color: #000000;
                }

        .reportarea {
            background-color: #F9F9F9;
        }
    </style>


</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">




    <script type="text/javascript">
        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(GetMenu);



        });


        function GetMenu() {


            try {

                var Moduleid = $('#<%=this.txtmodid.ClientID%>').val();
                GetModuleHeader(Moduleid);
                var leftul = $('#Leftul');
                leftul.html('');


                var Midul = $('#Midul');
                Midul.html('');
                var Rightul = $('#Rightul');
                Rightul.html('');

                //var Moduleid = "27";
                var lInputName = "02%";
                var MInputName = "03%";
                var RInputName = "04%";

                var Moduleobj = new RealERPScript();
                var leftlist = Moduleobj.GetModule(Moduleid, lInputName);
                var Midtlist = Moduleobj.GetModule(Moduleid, MInputName);
                var Righttlist = Moduleobj.GetModule(Moduleid, RInputName);
                // console.log(Righttlist);
                /*left , Middle and Right UL */
                $.each(leftlist, function (index, leftlist) {
                    if (leftlist.itemslct == false) {
                        leftul.append('<li><h5>' + leftlist.itemdesc + '</h5></li>');

                    }
                    else if (leftlist.itemslct == true && leftlist.itemdesc == "") {
                        leftul.append('');
                    }

                    else {

                        leftul.append('<li><a href=' + encodeURI(leftlist.itemurl) + '>' + leftlist.itemdesc + '</a></li>');
                    }


                });


                $.each(Midtlist, function (index, Midtlist) {
                    if (Midtlist.itemslct == false) {
                        Midul.append('<li><h5>' + Midtlist.itemdesc + '</h5></li>');

                    }
                    else if (Midtlist.itemslct == true && Midtlist.itemdesc == "") {
                        ;
                    }

                    else {

                        Midul.append('<li><a href=' + encodeURI(Midtlist.itemurl) + '>' + Midtlist.itemdesc + '</a></li>');
                    }


                });



                $.each(Righttlist, function (index, Righttlist) {
                    if (Righttlist.itemslct == false) {
                        Rightul.append('<li><h5>' + Righttlist.itemdesc + '</h5></li>');

                    }
                    else if (Righttlist.itemslct == true && Righttlist.itemdesc == "") {

                    }

                    else {
                        Rightul.append('<li><a href=' + encodeURI(Righttlist.itemurl) + '>' + Righttlist.itemdesc + '</a></li>');
                    }


                });

                /* for Mid dynamic accordion*/
                //var Middulacc = $('#accordionExample1');
                //Middulacc.html('');
                //var num = 1;
                //var status = "show";
                //var nextloop_m = Midtlist;
                //console.log(nextloop_m);
                //$.each(Midtlist, function (index, Midtlist) {
                //    if (Midtlist.itemslct == false) {
                //        if (num > 1) {
                //            status = "";
                //        }

                //        var newbind_m = '';
                //        Middulacc.append('<div class="card">' +
                //            '<div class="card-header" id="heading' + num + '">' +
                //            '<h5 class="mb-0">' +
                //            '<button class="btn-block text-left" type="button" data-toggle="collapse" data-target="#collapse1' + num + '" aria-expanded="true" aria-controls="collapse1' + num + '">' +
                //            Midtlist.itemdesc +
                //            '</button> </h5></div>' +
                //            '<div id="collapse1' + num + '" class="collapse  fade" aria-labelledby="heading' + num + '" data-parent="#accordionExample1">' +
                //            '<div class="card-body"> <ul class="list-group" id="ulm' + num + '">' +



                //            '</ul></div></div></div>');
                //        $.each(nextloop_m, function (index1, nextloop_m) {
                //            if (index1 > index) {
                //                if (nextloop_m.itemslct == true && nextloop_m.itemdesc != "") {
                //                    newbind_m += '<li><a href=' + encodeURI(nextloop_m.itemurl) + '>' + nextloop_m.itemdesc + '</a></li>';
                //                }
                //                else {
                //                    return false
                //                }
                //            }


                //        }),
                //            $("#ulm" + num + "").append(newbind_m);
                //        num += 1;
                //    }

                //    else if (Midtlist.itemslct == true && num == 1) {
                //        $("#repulMid").append('<li><a href=' + encodeURI(Midtlist.itemurl) + '>' + Midtlist.itemdesc + '</a></li>');
                //    }

                //});


                ///* for Right dynamic accordion*/
                //var Rightulacc = $('#accordionExample');
                //Rightulacc.html('');
                //var num = 1;
                //var status = "show";
                //var nextloop = Righttlist;
                //console.log(nextloop);
                //$.each(Righttlist, function (index, Righttlist) {
                //    if (Righttlist.itemslct == false) {
                //        if (num > 1) {
                //            status = "";
                //        }

                //        var newbind = '';
                //        Rightulacc.append('<div class="card">' +
                //            '<div class="card-header" id="heading' + num + '">' +
                //            '<h5 class="mb-0">' +
                //            '<button class="btn-block text-left" type="button" data-toggle="collapse" data-target="#collapse' + num + '" aria-expanded="true" aria-controls="collapse' + num + '">' +
                //            Righttlist.itemdesc +
                //            '</button> </h5></div>' +
                //            '<div id="collapse' + num + '" class="collapse  fade" aria-labelledby="heading' + num + '" data-parent="#accordionExample">' +
                //            '<div class="card-body"> <ul class="list-group" id="ul' + num + '">' +



                //            '</ul></div></div></div>');
                //        $.each(nextloop, function (index1, nextloop) {
                //            if (index1 > index) {
                //                if (nextloop.itemslct == true && nextloop.itemdesc != "") {
                //                    newbind += '<li><a href=' + encodeURI(nextloop.itemurl) + '>' + nextloop.itemdesc + '</a></li>';
                //                }
                //                else {
                //                    return false
                //                }
                //            }


                //        }),
                //            $("#ul" + num + "").append(newbind);
                //        num += 1;
                //    }
                   
                //    else if (Righttlist.itemslct == true && num == 1) {
                //        $("#repul").append('<li><a href=' + encodeURI(Righttlist.itemurl) + '>' + Righttlist.itemdesc + '</a></li>');
                //    }

                //});

                /* end left , Middle and Right UL */
                leftul.show();
                Midul.show();
                Rightul.show();
                // alert(num);

            }

            catch (e) {

                alert(e);
            }

        }



        function GetModuleHeader(Moduleid) {


            try {

                switch (Moduleid) {
                    case "35":
                        $('#leftmheader').html("A. Onetime Input");
                        $('#midmheader').html("B. Edit Facility");
                        $('#rightmheader').html("C. Admin Permission");
                        break;

                    default:

                        $('#leftmheader').html("A. Operational Menu");
                        $('#midmheader').html("B. General Report");
                        $('#rightmheader').html("C. Management Report");
                }
            }

            catch (e) {

                alert(e.message);


            }


        }


    </script>



    <div style="width: 99%; margin:auto;">

        <div class="contaier-fluid lbl2SubMenu headTagh3 moduleItemWrp cstepopertion">
            <div class="row">
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlModuleName" class="form-control ClCompAndMod" runat="server" TabIndex="2" AutoPostBack="True" OnSelectedIndexChanged="ddlModuleName_SelectedIndexChanged"></asp:DropDownList>

                </div>
                <div class="col-md-4">
                    <h2>
                        <asp:Label ID="modulenam" runat="server">Quick Tour</asp:Label></h2>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlCompanyName" class="form-control ClCompAndMod" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCompanyName_SelectedIndexChanged" TabIndex="1"></asp:DropDownList>
                </div>



            </div>
            <div class="row">
                <div class="col-md-4">
                    <h3 id="leftmheader">A. Operational Menu</h3>
                    <ul id="Leftul" class="nav colLeft">
                    </ul>

                </div>
                <div class="col-md-4">
                    <h3 id="midmheader">B. General Report</h3>
                    <ul id="Midul" class="nav colMid">
                    </ul>

                    <%--  <ul id="repulMid" class="reportarea">
                    </ul>
                    <div class="accordion reportarea" id="accordionExample1">
                    </div>--%>


                </div>
                <div class="col-md-4 ">
                    <h3 id="rightmheader">C. Management Report</h3>
                      <ul id="Rightul" class="nav colRight">


                </ul>
                      <%-- <ul id="repul" class="reportarea">
                 </ul>
                    <div class="accordion reportarea" id="accordionExample">
                    </div>--%>

                </div>
            </div>
            <asp:TextBox ID="txtmodid" runat="server" Style="display: none;"></asp:TextBox>
        </div>
    </div>
</asp:Content>



