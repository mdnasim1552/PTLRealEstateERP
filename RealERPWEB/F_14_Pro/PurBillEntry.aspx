<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="PurBillEntry.aspx.cs" Inherits="RealERPWEB.F_14_Pro.PurBillEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .minheight {
            min-height: 585px;
        }

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
    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard<a href="PurBillEntry.aspx">PurBillEntry.aspx</a>
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            var gridview = $('#<%=this.gvBillInfo.ClientID %>');
            $.keynavigation(gridview);
            //$(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
            $('.chzn-select').chosen({ search_contains: true });
            //$(".chzn-select").chosen();
            //$(".chzn-select-deselect").chosen({ allow_single_deselect: true });

        };

        function activetab() {
            var plug = $('#<%=this.txtflag.ClientID %>').val();
            // alert(plug);
            switch (plug) {
                case "IS":
                    $('.nav-tabs a[href="#tab1primary"]').tab('show');
                    break;
                case "BS":
                    $("#OpDate").hide();
                    $('.nav-tabs a[href="#tab0primary"]').tab('show');
                    break;
                case "SHEQUITY":
                    $("#OpDate").hide();
                    $('.nav-tabs a[href="#tab2primary"]').tab('show');
                    break;
                case "CSHFLW":
                    $("#OpDate").show();
                    $('.nav-tabs a[href="#tab3primary"]').tab('show');
                    break;
                case "CSHFLW2":
                    $("#OpDate").show();
                    $('.nav-tabs a[href="#tab4primary"]').tab('show');
                    break;

                case "Prjwiseres":
                    $("#OpDate").show();
                    $('.nav-tabs a[href="#tab5primary"]').tab('show');
                    break;
            }
        }
        function Confirmation() {
            if (confirm('Are you sure you want to save?')) {
                return;
            } else {
                return false;
            }
        }
        function activetab() {

            var plug = $('#<%=this.txtflag.ClientID %>').val();
            // alert(plug);
            switch (plug) {
                case "IS":
                    $('.nav-tabs a[href="#tab1primary"]').tab('show');
                    break;
                case "BS":
                    $("#OpDate").hide();
                    $('.nav-tabs a[href="#tab0primary"]').tab('show');
                    break;
                case "SHEQUITY":
                    $("#OpDate").hide();
                    $('.nav-tabs a[href="#tab2primary"]').tab('show');
                    break;
                case "CSHFLW":
                    $("#OpDate").show();
                    $('.nav-tabs a[href="#tab3primary"]').tab('show');
                    break;
                case "CSHFLW2":
                    $("#OpDate").show();
                    $('.nav-tabs a[href="#tab4primary"]').tab('show');
                    break;

                case "Prjwiseres":
                    $("#OpDate").show();
                    $('.nav-tabs a[href="#tab5primary"]').tab('show');
                    break;
            }
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <div class="container moduleItemWrpper minheight">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName">Bill Date</asp:Label>
                                        <asp:TextBox ID="txtCurBillDate" runat="server" CssClass=" inputtextbox" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurBillDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtCurBillDate"></cc1:CalendarExtender>

                                    </div>
                                    <div class="col-md-4 pading5px">
                                        <asp:Label ID="Label8" runat="server" CssClass="smLbl" Text="Bill No."></asp:Label>
                                        <asp:Label ID="lblCurBillNo1" runat="server"
                                            Text="PBL00-" CssClass=" smltxtBox"></asp:Label>
                                        <asp:TextBox ID="txtCurBillNo2" runat="server" ReadOnly="True" CssClass=" smltxtBox60px disabled">00000</asp:TextBox>
                                        <asp:Label ID="lblrefNo" runat="server" CssClass="smLbl">Ref. No.:</asp:Label>
                                        <asp:TextBox ID="txtBillRef" runat="server" CssClass=" inputtextbox" Style="width: 100px;"></asp:TextBox>

                                    </div>

                                    <div class="col-md-3 pading5px">
                                        <asp:Label ID="lblmsg1" CssClass="btn-danger btn  primaryBtn" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblaudit" runat="server" CssClass="lblTxt lblName" Visible="false">Audit Date</asp:Label>
                                        <asp:TextBox ID="txtauditbilldate" runat="server" CssClass=" inputtextbox" ToolTip="(dd-MMM-yyyy)" Visible="false"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtauditbilldate"></cc1:CalendarExtender>

                                    </div>
                                    <div class="col-md-2 pading5px asitCol2 pull-right">
                                        <asp:LinkButton ID="lbtnPrevBillList" runat="server" CssClass="lblTxt lblName" OnClick="lbtnPrevBillList_Click"
                                            TabIndex="3">
                                            Previous List</asp:LinkButton>

                                        <asp:DropDownList ID="ddlPrevBillList" runat="server" CssClass="chzn-select inputTxt inpPixedWidth" TabIndex="6">
                                        </asp:DropDownList>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblSupplier" runat="server" CssClass="lblTxt lblName">Supplier</asp:Label>
                                        <asp:TextBox ID="txtsrchsupplier" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="lbtnSupplierList" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="lbtnSupplierList_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-4 pading5px ">
                                        <asp:DropDownList ID="ddlSupList" runat="server" CssClass=" form-control inputTxt chzn-select " Width="350px">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1 pading5px asitCol1">
                                        <div class="colMdbtn pading5px">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <asp:Panel ID="Panel1" runat="server" Visible="False">
                            <fieldset class="scheduler-border">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label10" runat="server" CssClass="lblTxt lblName">Order</asp:Label>
                                            <asp:TextBox ID="txtSrchOrderrefno" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="imgSearchOrderno" runat="server" CssClass="btn btn-primary srearchBtn " OnClick="imgSearchOrderno_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                        </div>
                                        <div class="col-md-4 pading5px ">
                                            <asp:DropDownList ID="ddlOrderList" runat="server" AutoPostBack="true" CssClass="form-control inputTxt"
                                                OnSelectedIndexChanged="ddlOrderList_SelectedIndexChanged">
                                            </asp:DropDownList>

                                        </div>
                                        <div class="col-md-4 pading5px ">
                                            <asp:CheckBox ID="chkCharging" runat="server" AutoPostBack="True"
                                                OnCheckedChanged="chkCharging_CheckedChanged" Text="Charging" ForeColor="Green" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblmrrlist" runat="server" CssClass="lblTxt lblName">MRR List</asp:Label>
                                            <asp:TextBox ID="txtsrchmrr" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-primary srearchBtn "><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4 pading5px ">
                                            <asp:DropDownList ID="ddlMRRList" runat="server" CssClass="form-control inputTxt">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-md-4 pading5px ">
                                            <asp:LinkButton ID="lbtnSelectMRR" runat="server" ForeColor="Green" CssClass="btn btn-sm" OnClick="lbtnSelectMRR_Click">Select MRR</asp:LinkButton>

                                            <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Green" CssClass="btn btn-sm" OnClick="lbtnSelectRes_Click">Select Material</asp:LinkButton>

                                            <asp:LinkButton ID="lbtnSelectAll" runat="server" ForeColor="Green" CssClass="btn btn-sm" OnClick="lbtnSelectAll_Click">Select All</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </asp:Panel>

                        <asp:Panel ID="PnlCharging" runat="server" Visible="False">
                            <fieldset class="scheduler-border ">

                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label11" runat="server" CssClass="lblTxt lblName">Project Name</asp:Label>
                                            <asp:TextBox ID="txtSrchProjectName" runat="server" CssClass=" inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="imgSearchProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgSearchProject_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>
                                            </div>
                                        </div>

                                        <div class="col-md-4 pading5px ">
                                            <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control inputTxt">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-md-3 pading5px asitCol3">
                                            <div class="colMdbtn pading5px">
                                                <asp:LinkButton ID="lbtnSelect" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnSelect_Click">Select</asp:LinkButton>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label12" runat="server" CssClass="lblTxt lblName">Charge</asp:Label>
                                            <asp:TextBox ID="txtSrchCharge" runat="server" CssClass=" inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="imgSearchCharge" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgSearchCharge_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>
                                            </div>
                                        </div>

                                        <div class="col-md-4 pading5px">
                                            <asp:DropDownList ID="ddlCharge" runat="server" CssClass="form-control inputTxt">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblvalvounum" runat="server" CssClass="lblTxt lblName" Visible="false"></asp:Label>

                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </asp:Panel>
                    </div>
                    <div class="row table-responsive">
                        <asp:GridView ID="gvBillInfo" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" ShowFooter="True" Width="16px"
                            OnRowDeleting="gvBillInfo_RowDeleting" CssClass="table table-striped table-hover table-bordered grvContentarea">
                            <PagerSettings Visible="False" />
                            <RowStyle />

                            <Columns>
                                <asp:TemplateField HeaderText="Sl" HeaderStyle-Width="20px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="20px" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Project">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvProject" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Req No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvReqNo1" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Order No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvOrderno" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1")) %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Order Ref">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvorderissueno" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "oissueno")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="MRR No.">

                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdateBill" runat="server" Font-Bold="True" OnClientClick="return Confirmation();"
                                            OnClick="lbtnUpdateBill_Click" CssClass="btn btn-danger primarygrdBtn   ">
                                            Update</asp:LinkButton>

                                    </FooterTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMRRNo1" runat="server" CssClass="textwrap"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno1")) %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="MRR Ref">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnDeleteBill" runat="server" OnClick="lbtnDeleteBill_Click"
                                            CssClass="btn btn-primary primarygrdBtn">
                                            Delete All</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMrrref" runat="server" CssClass="textwrap"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrref")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Chalan #">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnFinalUpdateBill" runat="server" Font-Bold="True" OnClientClick="return Confirmation();"
                                            OnClick="lbtnFinalUpdateBill_Click" CssClass="btn btn-danger primarygrdBtn" Visible="false">
                                            Approved</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvchalanno" runat="server" CssClass=" textwrap"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chlnno")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Chalan Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvchallandat" runat="server" CssClass="textwrap"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "challandat")).ToString()%>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:CommandField ShowDeleteButton="True" />



                                <asp:TemplateField HeaderText="Description of Materials">
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlPageNo" runat="server" __designer:wfdid="w67"
                                            AutoPostBack="True" Font-Bold="True" Font-Size="14px"
                                            OnSelectedIndexChanged="ddlPageNo_SelectedIndexChanged"
                                            Style="border-right: navy 1px solid; border-top: navy 1px solid; border-left: navy 1px solid; border-bottom: navy 1px solid"
                                            Width="150px">
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvResDesc" runat="server"
                                            Text='<%# "<B>" + Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")) + "</B>" +
                                                                         (DataBinder.Eval(Container.DataItem, "spcfdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")).Trim(): "")   %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnTotal" runat="server"
                                            OnClick="lbtnTotal_Click" CssClass="btn btn-primary primarygrdBtn">
                                            Total</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvResUnit" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                            Width="35px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MRR Qty.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMRRQty" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMRRRate" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Bill Amount (Mgt)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvmMRRAmt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mmrramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFMMRRAmt" runat="server" Width="70px" Font-Bold="True"
                                            Font-Size="12px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="11px" HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Bill Amount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvMRRAmt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px">
                                        </asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFooterTMRRAmt" runat="server" Width="70px" Font-Bold="True"
                                            Font-Size="12px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="11px" HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRemarks" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remrks")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--Item Serial RowID add for Manama--%>
                                <asp:TemplateField HeaderText="Item Sl" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvItemSl" runat="server" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"rowid")) %>' Width="15px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                        <div class="clearfix"></div>
                    </div>





                    <div class="row">
                        <asp:Panel ID="Panel2" runat="server" Visible="False">
                            <fieldset class="scheduler-border fieldset_Nar">
                                <div class="form-horizontal">


                                    <div class="form-group">

                                        <div class="col-md-12  pading5px">
                                            <asp:Label ID="lblsecurity" runat="server" CssClass="lblTxt lblName" Text="Security Deposit:"></asp:Label>
                                            <asp:TextBox ID="txtpercentage" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                            <asp:LinkButton ID="lbtnDepost" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnDepost_Click">Amt</asp:LinkButton>
                                            <asp:TextBox ID="txtSDAmount" runat="server" CssClass="inputtextbox" Style="text-align: right;"></asp:TextBox>
                                            <asp:Label ID="lbldeduction" runat="server"
                                                Text="Deduction" CssClass=" smLbl_to"></asp:Label>
                                            <asp:TextBox ID="txtDedAmount" runat="server" CssClass="inputtextbox" Style="text-align: right;"></asp:TextBox>
                                            <asp:Label ID="lblpenalty" runat="server"
                                                Text="Penalty" CssClass=" smLbl_to"></asp:Label>
                                            <asp:TextBox ID="txtPenaltyAmount" runat="server" CssClass="inputtextbox" Style="width: 60px; text-align: right;"></asp:TextBox>

                                            <asp:Label ID="lblAdvanced" runat="server"
                                                Text="Advanced" CssClass=" smLbl_to"></asp:Label>
                                            <asp:TextBox ID="txtAdvanced" runat="server" CssClass="inputtextbox" Style="width: 60px; text-align: right;"></asp:TextBox>




                                            <asp:Label ID="lblnettotal" runat="server"
                                                Text="Net Total:" CssClass=" smLbl_to"></asp:Label>

                                            <asp:Label ID="lblvalnettotal" runat="server" CssClass="smLbl_to" Style="text-align: right; color: blue;"></asp:Label>
                                        </div>

                                    </div>





                                    <div class="form-group">
                                        <div class="col-md-6 pading5px">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="lblReqNarr" runat="server" CssClass="lblTxt" Text="Narration:"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtBillNarr" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>






                                    </div>


                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3 ">
                                            <asp:Label ID="lbladjjustment" runat="server" CssClass="lblTxt lblName">Adjustment</asp:Label>



                                            <asp:DropDownList ID="ddlPayType" runat="server" CssClass="smDropDown inputTxt"
                                                OnSelectedIndexChanged="ddlPayType_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="001">None</asp:ListItem>
                                                <asp:ListItem Value="003">Adjustment</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                        <div class="col-md-4 pading5px" runat="server" visible="True" id="datearea">

                                            <asp:Label ID="lblbillrefdate" runat="server" CssClass="smLbl">Bill Date</asp:Label>
                                            <asp:TextBox ID="txtBillrefDate" runat="server" CssClass="inputtextbox" ToolTip="(dd.mm.yyyy)"></asp:TextBox>


                                            <cc1:CalendarExtender ID="txtBillrefDate_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtBillrefDate"></cc1:CalendarExtender>
                                            <asp:Label ID="lblchequedate" runat="server" CssClass="smLbl">Chq.Date</asp:Label>
                                            <asp:TextBox ID="txtChequeDate" runat="server" CssClass="inputtextbox" ToolTip="(dd.mm.yyyy)"></asp:TextBox>


                                            <cc1:CalendarExtender ID="txtChequeDate_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtChequeDate"></cc1:CalendarExtender>

                                        </div>

                                        <div class="col-md-2 pading5px">
                                        </div>

                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-3 pading5px">
                                            <asp:Label ID="lblPreparedBy" runat="server" CssClass="lblTxt lblName" Text="Prepared By:" Visible="False"></asp:Label>
                                            <asp:TextBox ID="txtPreparedBy" runat="server" CssClass="inputTxt inputName inpPixedWidth" ToolTip="(dd.mm.yyyy)" Visible="False"></asp:TextBox>


                                        </div>

                                        <div class="col-md-3 pading5px">


                                            <asp:Label ID="lblApprovedBy" runat="server" CssClass="lblTxt lblName" Text="Approved By:" Visible="False"></asp:Label>
                                            <asp:TextBox ID="txtApprovedBy" runat="server" CssClass="inputTxt inputName inpPixedWidth" ToolTip="(dd.mm.yyyy)" Visible="False"></asp:TextBox>


                                        </div>

                                        <div class="col-md-3 pading5px">


                                            <asp:Label ID="lblApprovalDate" runat="server" CssClass="lblTxt lblName" Text="Approv.Date:" Visible="False"></asp:Label>
                                            <asp:TextBox ID="txtApprovalDate" runat="server" CssClass="inputTxt inputName inpPixedWidth" ToolTip="(dd.mm.yyyy)" Visible="False"></asp:TextBox>


                                        </div>

                                        <div class="col-md-3 pading5px">


                                            <asp:Label ID="lblVounum" runat="server" CssClass="lblTxt lblName" Text="Acc. Vou. No." Visible="False"></asp:Label>
                                            <asp:TextBox ID="txtAccVounum" runat="server" CssClass="inputTxt inputName inpPixedWidth" ToolTip="(dd.mm.yyyy)" Visible="False"></asp:TextBox>
                                            <asp:TextBox ID="txtflag" Style="display: none;" runat="server"></asp:TextBox>

                                        </div>




                                    </div>

                                </div>

                            </fieldset>
                            <%--  <fieldset class="scheduler-border fieldset_Nar">
                            <div class="form-horizontal">
                                
                                <div class="form-group">
                                                    <div class="col-md-4 col-sm-4 col-lg-4">
                                                        <asp:Panel runat="server" ID="pnlQutatt">


                                                            <div class="panel panel-primary hidden">
                                                                <div class="panel-heading">
                                                                    <span class="glyphicon glyphicon-upload"></span> BILL Documents Upload
   
                                                                </div>
                                                                <div class="panel-body">
                                                                    <div class="row">
                                                                        <div class="col-lg-12"> 
                                                                            <div class="row">
                                                                                <fieldset class="alert alert-success">

                                                                                    <cc1:AsyncFileUpload OnClientUploadError="uploadError"
                                                                                        OnClientUploadComplete="uploadComplete" runat="server"
                                                                                        ID="AsyncFileUpload1" UploaderStyle="Modern"
                                                                                        CompleteBackColor="White"
                                                                                        UploadingBackColor="#CCFFFF" ThrobberID="imgLoader"
                                                                                        OnUploadedComplete="FileUploadComplete" />
                                                                                    <asp:Image ID="imgLoader" runat="server" Visible="false" ImageUrl="~/images/Wait.gif" />
                                                                                    <br />
                                                                                    <asp:Label ID="lblMesg" runat="server" Text=""></asp:Label>
                                                                                </fieldset>


                                                                            </div>



                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>

                                                    </div>
                                                  <div class="col-md-7">
                                                        <div class="panel panel-primary">
                                                            <div class="panel-heading">
                                                                <span class="glyphicon glyphicon-picture"></span>Uploaded Files
       
                                            <div class="pull-right">
                                                <asp:Button ID="btnShowimg" runat="server" CssClass="btn btn-success btn-xs" Text="Show Image" OnClick="btnShowimg_Click" />
                                                <asp:LinkButton ID="btnDelall" runat="server" OnClick="btnDelall_OnClick" Visible="true" CssClass=" btn btn-xs btn-danger">Delete</asp:LinkButton>

                                            </div>
                                                            </div>
                                                            <div class="panel-body ">
                                                                <div class="row">
                                                                    <div class="col-lg-12">
                                                                        <asp:ListView ID="ListViewEmpAll" runat="server" ItemPlaceholderID="itemplaceholder" OnItemDataBound="ListViewEmpAll_ItemDataBound">
                                                                            <LayoutTemplate>
                                                                                <asp:PlaceHolder ID="itemplaceholder" runat="server" />
                                                                            </LayoutTemplate>
                                                                            <ItemTemplate>
                                                                                <div class="col-xs-12 col-sm-4 col-md-2 listDiv" style="padding: 0 5px;">
                                                                                    <div id="EmpAll" runat="server">

                                                                                        <asp:Label ID="ImgLink" Visible="False" runat="server" Text='<%# Eval("itemsurl") %>'></asp:Label>
                                                                                        <asp:Label ID="billno" Visible="False" runat="server" Text='<%# Eval("billno") %>'></asp:Label>
                                                                                        <asp:Label ID="id" Visible="False" runat="server" Text='<%# Eval("id") %>'></asp:Label>

                                                                                        <a href="../../Upload/Purchase/<%# Eval("itemsurl") %>" class="uploadedimg" target="_blank">
                                                                                            <asp:Image ID="GetImg" runat="server" CssClass="image img img-responsive img-thumbnail" />
                                                                                        </a>
                                                                                        <div class="checkboxcls">
                                                                                            <asp:CheckBox ID="ChDel" runat="server" />
                                                                                        </div>


                                                                                    </div>

                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                            </div>
                            </fieldset>--%>
                        </asp:Panel>



                        <fieldset class="scheduler-border fieldset_Nar">
                            <div class="form-horizontal">

                                <div class="form-group">

                                    <div class="row">

                                        <div class="col-md-12">
                                            <div class="panel with-nav-tabs panel-default">
                                                <div class="panel-heading">
                                                    <ul class="nav nav-tabs" id="tabsbar" runat="server">
                                                        <li class="active"><a href="#tab0primary" class="tablink" onclick="activetab('Requisition');" data-toggle="tab" style="font-size: 16px"><span class="glyphicon glyphicon-list"></span>Requisition</a></li>
                                                        <li><a href="#tab1primary" class="tablink" onclick="activetab('Quotation');" data-toggle="tab" style="font-size: 16px"><span class="glyphicon glyphicon-list"></span>Work Order</a></li>

                                                        <li><a href="#tab3primary" class="tablink" onclick="activetab('Order');" data-toggle="tab" style="font-size: 16px"><span class="glyphicon glyphicon-list"></span>MRR </a></li>
                                                        <li><a href="#tab5primary" class="tablink" onclick="activetab('Mrr');" data-toggle="tab" style="font-size: 16px"><span class="glyphicon glyphicon-list"></span>Bill MRR</a></li>

                                                    </ul>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="tab-content">
                                                        <%-- <div class="tab-pane fade in active" id="tabreq">
                                        01. Requisition           

                                    </div>

                                     <div class="tab-pane fade in active" id="tabquotation">
                                         01. Quotation                   

                                    </div>

                                    
                                     <div class="tab-pane fade in active" id="tabworkorder">
                                         01. Work Order                   

                                    </div>

                                      <div class="tab-pane fade in active" id="tabmrrreceipt">
                                         01. Receipt                

                                    </div>
                                   
                                     <div class="tab-pane fade in active" id="tabbill">
                                         01. Bill                

                                    </div>--%>

                                                        <div class="col-md-4" id="imgpanel" runat="server">

                                                            <div class="row-fluid">

                                                                <div class="panel panel-primary">
                                                                    <div class="panel-heading">
                                                                        <span class="glyphicon glyphicon-picture"></span>Image Upload
                                    <div class="pull-right">
                                        <div class=" file-upload">

                                            <cc1:AsyncFileUpload OnClientUploadError="uploadError"
                                                OnClientUploadComplete="uploadComplete" runat="server"
                                                ID="AsyncFileUpload1" UploaderStyle="Modern"
                                                ThrobberID="imgLoader"
                                                OnUploadedComplete="AsyncFileUpload1_UploadedComplete" />
                                            <%--<span class="glyphicon glyphicon-upload">
                                                <asp:FileUpload runat="server" OnClientUploadError="uploadError" ID="AsyncFileUpload1"
                                                    OnClientUploadComplete="uploadComplete" OnUploadedComplete="FileUploadComplete" />
                                            </span>--%>


                                            <asp:Image ID="imgLoader" runat="server" Visible="False" ImageUrl="~/images/Wait.gif" />
                                            <br />
                                        </div>
                                        <asp:LinkButton ID="btnDelall" runat="server" OnClick="btnDelall_OnClick" OnClientClick="return confirm('Really Do You want to Delete This Image?')" CssClass=" btn btn-xs btn-danger">Delete</asp:LinkButton>
                                    </div>
                                                                    </div>
                                                                    <div class="panel-body">
                                                                        <div class="row">
                                                                            <div class="col-lg-12">
                                                                                <div class="row">
                                                                                    <div class="form-group">
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
                                                                                        <asp:Label ID="billno" Visible="false" runat="server" Text='<%# Eval("billno") %>'></asp:Label>

                                                                                        <a href="#" class="uploadedimg">

                                                                                            <asp:Image ID="GetImg" runat="server" CssClass="pop image img img-responsive img-thumbnail " Height="65px" />
                                                                                            <div class="middle">
                                                                                                <%--   <span><%# Eval("pactcode") %></span>--%>
                                                                                            </div>
                                                                                            <div class="checkboxcls">
                                                                                                <asp:CheckBox ID="ChDel" Style="margin: 0px 80px; padding: 0px;" runat="server" />
                                                                                            </div>
                                                                                        </a>
                                                                                    </div>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </div>
                                                                </div>


                                                            </div>



                                                        </div>






                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                </div>
                            </div>
                        </fieldset>


                    </div>
                </div>
                <!-- End of contentpart-->
            </div>
            <!-- End of Container-->


            <%-- <script type="text/javascript">
            function uploadComplete(sender) {
                $get("<%=lblMesg.ClientID%>").style.color = "green";
                $get("<%=lblMesg.ClientID%>").innerHTML = "File Uploaded Successfully";
            }

            function uploadError(sender) {
                $get("<%=lblMesg.ClientID%>").style.color = "red";
                $get("<%=lblMesg.ClientID%>").innerHTML = "File upload failed.";
            }


        </script>--%>
        </ContentTemplate>
    </asp:UpdatePanel>



    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {


            $(".pop").on("click", function () {
                $('#imagepreview').attr('src', $(this).attr('src')); // here asign the image to the modal when the user click the enlarge link
                $('#imagemodal').modal('show'); // imagemodal is the id attribute assigned to the bootstrap modal, then i use the show function
            });
        }
    </script>

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

