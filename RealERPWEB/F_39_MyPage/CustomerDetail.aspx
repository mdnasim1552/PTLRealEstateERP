<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="CustomerDetail.aspx.cs" Inherits="RealERPWEB.F_39_MyPage.CustomerDetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" integrity="sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p" crossorigin="anonymous"/>
    <style>
        .lblHead {
            color: blue;
            font-size: 14px !important;
            font-weight: bold;
        }

        .table {
            margin-bottom: 0;
        }

        .middle {
            transition: .5s ease;
            opacity: 0;
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            -ms-transform: translate(-50%, -50%);
        }

        .checkboxcls {
            opacity: 1;
            position: absolute;
            top: 80%;
            left: 10%;
        }

        .uploadedimg .image {
            opacity: 1;
            display: block;
            width: 100%;
            height: auto;
            transition: .5s ease;
            backface-visibility: hidden;
        }

        .uploadedimg:hover .image {
            opacity: 0.3;
        }

        .uploadedimg:hover .middle {
            opacity: 1;
        }
        /**modal */

        #myImg {
            border-radius: 5px;
            cursor: pointer;
            transition: 0.3s;
        }

            #myImg:hover {
                opacity: 0.7;
            }

        /* The Modal (background) */
        .modal {
            display: none; /* Hidden by default */
            position: fixed; /* Stay in place */
            z-index: 1; /* Sit on top */
            padding-top: 100px; /* Location of the box */
            left: 0;
            top: 0;
            width: 100%; /* Full width */
            height: 100%; /* Full height */
            overflow: auto; /* Enable scroll if needed */
            background-color: rgb(0,0,0); /* Fallback color */
            background-color: rgba(0,0,0,0.9); /* Black w/ opacity */
        }

        /* Modal Content (image) */
        .modal-content {
            margin: auto;
            display: block;
            width: 80%;
            max-width: 700px;
        }

        /* Caption of Modal Image */
        #caption {
            margin: auto;
            display: block;
            width: 80%;
            max-width: 700px;
            text-align: center;
            color: #ccc;
            padding: 10px 0;
            height: 150px;
        }

        /* Add Animation */
        .modal-content, #caption {
            -webkit-animation-name: zoom;
            -webkit-animation-duration: 0.6s;
            animation-name: zoom;
            animation-duration: 0.6s;
        }

        @-webkit-keyframes zoom {
            from {
                -webkit-transform: scale(0);
            }

            to {
                -webkit-transform: scale(1);
            }
        }

        @keyframes zoom {
            from {
                transform: scale(0);
            }

            to {
                transform: scale(1);
            }
        }

        /* The Close Button */
        .close {
            position: absolute;
            top: 69px;
            right: 298px;
            color: #e8c5cf;
            font-size: 40px;
            font-weight: bold;
            transition: 0.3s;
        }

            .close:hover,
            .close:focus {
                color: #bbb;
                text-decoration: none;
                cursor: pointer;
            }

        /* 100% Image Width on Smaller Screens */
        @media only screen and (max-width: 700px) {
            .modal-content {
                width: 100%;
            }
        }

        .file-upload {
            display: inline-block;
            overflow: hidden;
            text-align: center;
            vertical-align: middle;
            font-family: Arial;
            border: 1px solid #124d77;
            background: #007dc1;
            color: #fff;
            border-radius: 6px;
            -moz-border-radius: 6px;
            cursor: pointer;
            text-shadow: #000 1px 1px 2px;
            -webkit-border-radius: 6px;
        }

            .file-upload:hover {
                background: -webkit-gradient(linear, left top, left bottom, color-stop(0.05, #0061a7), color-stop(1, #007dc1));
                background: -moz-linear-gradient(top, #0061a7 5%, #007dc1 100%);
                background: -webkit-linear-gradient(top, #0061a7 5%, #007dc1 100%);
                background: -o-linear-gradient(top, #0061a7 5%, #007dc1 100%);
                background: -ms-linear-gradient(top, #0061a7 5%, #007dc1 100%);
                background: linear-gradient(to bottom, #0061a7 5%, #007dc1 100%);
                filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#0061a7', endColorstr='#007dc1',GradientType=0);
                background-color: #0061a7;
            }

        /* The button size */
        .file-upload {
            height: 30px;
        }

            .file-upload, .file-upload span {
                width: 50px;
            }

                .file-upload input {
                    top: 0;
                    left: 0;
                    margin: 0;
                    font-size: 11px;
                    font-weight: bold;
                    /* Loses tab index in webkit if width is set to 0 */
                    /*opacity: 0;
            filter: alpha(opacity=0);*/
                }

                .file-upload strong {
                    font: normal 12px Tahoma,sans-serif;
                    text-align: center;
                    vertical-align: middle;
                }

                .file-upload span {
                    top: 0;
                    left: 0;
                    display: inline-block;
                    /* Adjust button text vertical alignment */
                    padding-top: 5px;
                }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row" style="display: none;">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblClientName" runat="server" CssClass="lblTxt lblName">Client Name</asp:Label>
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputtextbox"></asp:TextBox>



                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>

                                    </div>

                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlPrjName" runat="server" CssClass="form-control inputTxt">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblProjectdesc" runat="server" CssClass="form-control inputTxt" Visible="false"></asp:Label>



                                    </div>

                                    <div class="col-md-1">

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                        </div>

                                    </div>
                                    <div class="col-md-3 pading5px pull-right">

                                        <div class="msgHandSt">
                                            <asp:Label ID="lmsg" CssClass="btn-danger btn disabled" runat="server" Visible="false"></asp:Label>
                                        </div>


                                    </div>

                                    <asp:Label ID="lblcustomerid" runat="server" CssClass="form-control inputTxt" Visible="false"></asp:Label>
                                </div>
                            </div>
                        </fieldset>

                    </div>

                    <div class="row">
                        <div class="col-md-8">
                            <asp:GridView ID="gvPersonalInfo" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="831px" Visible="True" OnRowDataBound="gvPersonalInfo_RowDataBound">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right; font-size: 12px;"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"
                                                ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                Width="49px" ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcResDesc1" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                Width="220px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvgval" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <FooterTemplate>

                                            <asp:LinkButton ID="lUpdatPerInfo" runat="server" CssClass="btn btn-warning primaryBtn" OnClick="lUpdatPerInfo_Click">Update Personal Info</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent" AutoCompleteType="Disabled"
                                                Width="130px" BorderStyle="None"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                Height="20px" Font-Size="12px"></asp:TextBox>

                                            <asp:TextBox
                                                ID="txtgvdVal" runat="server" BackColor="Transparent" BorderStyle="None" AutoCompleteType="Disabled"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                Width="130px" Font-Size="12px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender_txtgvdVal" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdVal"></cc1:CalendarExtender>

                                            <asp:DropDownList ID="ddlFileNo" runat="server" CssClass="form-control inputTxt"></asp:DropDownList>

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField>

                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvValbn" runat="server" BackColor="Transparent"
                                                Width="130px" BorderStyle="None"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc2")) %>'
                                                Height="20px" Font-Size="12px"></asp:TextBox>

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                </Columns>


                                <%--<FooterStyle CssClass="grvFooter" />--%>
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <FooterStyle BackColor="#23cc94" />
                                <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" Height="30px" />
                            </asp:GridView>
                        </div>
                        <div class="col-md-4" id="divAttachedfiles" runat="server">
                            <div class="row">
                                   <div class="col-md-12">
                            <a href="../F_22_Sal/ProjectFileDetailsEntry"  class="btn btn-success"  target="_blank" ><i class="fas fa-layer-plus">&nbsp;Add Files</i></a>
                            <%--<asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-success" NavigateUrl="~/F_22_Sal/ProjectFileDetailsEntry"><i class="fas fa-layer-plus">&nbsp;Add Files</i></asp:LinkButton>--%>
                        </div>
                        <div class="col-md-12">
                            <div class="row-fluid">
                                <div class="panel panel-primary">
                                    <div class="panel-heading">
                                        <span class="glyphicon glyphicon-picture"></span>Image Upload
                                    <div class="pull-right">
                                        <asp:LinkButton ID="btnDelall" runat="server" OnClick="btnDelall_OnClick" OnClientClick="return confirm('Really Do You want to Delete This Image?')" CssClass=" btn btn-xs btn-danger">Delete</asp:LinkButton>
                                    </div>
                                    </div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="row">
                                                    <div class="form-group">

                                                        <asp:Label ID="Label2" runat="server" CssClass="col-md-3" Text="Person"></asp:Label>
                                                        <div class="col-md-6">
                                                            <asp:DropDownList ID="ddlimgperson" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlimgperson_SelectedIndexChanged" AutoPostBack="true">
                                                                <asp:ListItem Selected="True">Customer</asp:ListItem>
                                                                <asp:ListItem>Nominee</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-3 file-upload">
                                                            <cc1:AsyncFileUpload OnClientUploadError="uploadError"
                                                                OnClientUploadComplete="uploadComplete" runat="server"
                                                                ID="AsyncFileUpload1" UploaderStyle="Modern"
                                                                CompleteBackColor="White"
                                                                UploadingBackColor="#CCFFFF" ThrobberID="imgLoader"
                                                                OnUploadedComplete="FileUploadComplete" />
                                                            <asp:Image ID="imgLoader" runat="server" Visible="false" ImageUrl="~/images/Wait.gif" />
                                                            <br />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <asp:Label ID="lblMesg" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:ListView ID="ListViewEmpAll" runat="server" ItemPlaceholderID="itemplaceholder" OnItemDataBound="ListViewEmpAll_ItemDataBound">
                                            <LayoutTemplate>
                                                <asp:PlaceHolder ID="itemplaceholder" runat="server" />
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <div class="col-xs-12 col-sm-4 col-md-4 listDiv" style="padding: 0 5px;">
                                                    <div id="EmpAll" runat="server">


                                                        <asp:Label ID="ImgLink" Visible="false" runat="server" Text='<%# Eval("imgpath") %>'></asp:Label>
                                                        <asp:Label ID="pactcode" Visible="false" runat="server" Text='<%# Eval("pactcode") %>'></asp:Label>
                                                        <asp:Label ID="usricode" Visible="false" runat="server" Text='<%# Eval("usircode") %>'></asp:Label>

                                                        <%--<a href="#" class="uploadedimg" target="_blank">--%>

                                                        <asp:Image ID="GetImg" runat="server" CssClass="custimg image img img-responsive img-thumbnail " Height="65px" />
                                                        <div class="middle">
                                                            <%--   <span><%# Eval("pactcode") %></span>--%>
                                                        </div>
                                                        <div class="checkboxcls">
                                                            <asp:CheckBox ID="ChDel" Style="margin: 0px 50px; padding: 0px;" runat="server" />
                                                        </div>
                                                        <%--</a>--%>
                                                    </div>

                                                </div>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- The Modal -->
                        <div class="col-md-12">
                            <div id="myModal" class="modal">
                                <span class="close">&times;</span>
                                <img class=" modal-content img img-responsive" id="img01">
                                <div id="caption"></div>
                            </div>
                        </div>
                            </div>
                        </div>
                     
                    </div>
                    <!-- End of contentpart-->



                    <%--<img id="myImg" class="custimg" src="http://localhost:17139/Upload/CUSTOMER/18010001000151010030200258021.jpg" alt="Snow" style="width:100%;max-width:300px">--%>



                    <script>
                        // Get the modal
                        $(".custimg").click(function () {
                            var modal = document.getElementById('myModal');
                            var modalImg = document.getElementById("img01");
                            var src = $(this).attr("src");
                            modal.style.display = "block";
                            modalImg.src = src;
                            // captionText.innerHTML = "slfajljfsfl";
                            // alert("ssf");
                        });
                        //var modal = document.getElementById('myModal');

                        //// Get the image and insert it inside the modal - use its "alt" text as a caption
                        //var img = document.getElementById('myImg');
                        //var modalImg = document.getElementById("img01");
                        //var captionText = document.getElementById("caption");
                        //img.onclick = function(){
                        //    modal.style.display = "block";
                        //    modalImg.src = this.src;
                        //    captionText.innerHTML = this.alt;
                        //}

                        // Get the <span> element that closes the modal
                        var span = document.getElementsByClassName("close")[0];

                        // When the user clicks on <span> (x), close the modal
                        span.onclick = function () {
                            var modal = document.getElementById('myModal');
                            modal.style.display = "none";
                        }
                    </script>


                </div>
                <!-- End of Container-->
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function uploadComplete(sender) {
            $get("<%=lblMesg.ClientID%>").style.color = "green";
            $get("<%=lblMesg.ClientID%>").innerHTML = "File Uploaded Successfully";
        }

        function uploadError(sender) {
            $get("<%=lblMesg.ClientID%>").style.color = "red";
            $get("<%=lblMesg.ClientID%>").innerHTML = "File upload failed.";
        }


    </script>

</asp:Content>






