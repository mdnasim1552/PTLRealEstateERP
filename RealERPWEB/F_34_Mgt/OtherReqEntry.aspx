<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="OtherReqEntry.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.OtherReqEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .AutoExtender {
            font-family: Verdana, Helvetica, sans-serif;
            margin: 0px 0 0 0px;
            font-size: 11px;
            font-weight: normal;
            border: solid 1px #006699;
            background-color: White;
        }

        .AutoExtenderList {
            border-bottom: dotted 1px #006699;
            cursor: pointer;
            color: Maroon;
        }

        .AutoExtenderHighlight {
            color: White;
            background-color: #006699;
            cursor: pointer;
        }

        .switch {
            position: relative;
            display: inline-block;
            width: 40px;
            height: 20px;
        }

            .switch input {
                opacity: 0;
                width: 0;
                height: 0;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 18px;
                width: 18px;
                left: 1px;
                bottom: 1px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(18px);
            -ms-transform: translateX(18px);
            transform: translateX(18px);
        }


        .slider.round {
            border-radius: 20px;
        }

            .slider.round:before {
                border-radius: 50%;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });
        };
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblCurDate" runat="server" Text="Req.Date"></asp:Label>
                                <asp:TextBox ID="txtCurReqDate" runat="server" CssClass="form-control" AutoPostBack="true" AutoCompleteType="Disabled"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtCurReqDate_CalendarExtender" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtCurReqDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblCurNo" CssClass="smLbl_to" runat="server" Text=" Req. No."></asp:Label>

                            <div class="row">
                                <div class="col-md-5">
                                    <asp:Label ID="lblCurReqNo1" CssClass="form-control" runat="server">000</asp:Label>
                                </div>
                                <div class="col-md-7">
                                    <asp:TextBox ID="txtCurReqNo2" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="lblmrfno" CssClass="smLbl_to" runat="server">Source Ref.</asp:Label>
                            <asp:TextBox ID="txtMRFNo" runat="server" CssClass="form-control"></asp:TextBox>

                        </div>

                        <div class="col-md-1">
                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" Style="margin-top: 20px"
                                OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lbldep" runat="server" CssClass="lblName lblTxt"  Text="Department"></asp:Label>
                         
                            <asp:DropDownList ID="ddlDeptCode" runat="server" CssClass="form-control chzn-select" >
                            </asp:DropDownList>
                           
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label5" CssClass="lblTxt lblName" runat="server">Print Type:</asp:Label>
                            <asp:RadioButtonList runat="server" ID="RbtnPrint" CssClass="form-control chkBoxControl" RepeatDirection="Horizontal">
                                <asp:ListItem Value="Requisition">Requisition</asp:ListItem>
                                <asp:ListItem Value="Order">Order</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>

                        <div class="col-md-1">
                            <label id="chkbod" runat="server" visible="false" class="switch">
                                <asp:CheckBox ID="chkAdvanced" runat="server" Visible="false" OnCheckedChanged="chkAdvanced_CheckedChanged" AutoPostBack="true" />
                                <span class="btn btn-xs slider round"></span>
                            </label>
                            <asp:Label runat="server" ID="lbladvanced" Visible="false" Text="Adjusted" CssClass="btn btn-xs"></asp:Label>
                        </div>

                        <div class="col-md-2">
                            <asp:LinkButton ID="lbtnPrevReqList" runat="server" CssClass="lblName lblTxt" OnClick="lbtnPrevReqList_Click" Text="Prev Req"></asp:LinkButton>
                            <asp:TextBox ID="txtSrchMrfNo" runat="server" TabIndex="2" CssClass="inputtextbox" Visible="false"></asp:TextBox>
                         
                            <asp:DropDownList ID="ddlPrevReqList" runat="server" CssClass="form-control chzn-select" TabIndex="3">
                            </asp:DropDownList>
                            <asp:Label ID="lblmsg1" runat="server" CssClass="btn btn-danger primaryBtn" Visible="false"></asp:Label>
                        </div>


                       
                    </div>
                </div>
            </div>

            <asp:Panel ID="Panel1" runat="server" Visible="False">
                <div class="card card-fluid">
                    <div class="card-body">
                        <asp:Panel ID="pnlnew" runat="server" Visible="False">
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="lblProjectname" runat="server" CssClass="lblTxt lblName" Text="Accounts Head:"></asp:Label>

                                    <asp:DropDownList ID="ddlProjectName" runat="server" Font-Bold="True" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" CssClass="form-control chzn-select" TabIndex="3">
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-2">
                                    <asp:Label ID="lblMatGroup" runat="server" CssClass="lblName lblTxt" Text="Resource Head"></asp:Label>
                                    <asp:TextBox ID="txtResgroup" runat="server" TabIndex="2" CssClass="inputtextbox" Visible="false"></asp:TextBox>
                                    <asp:LinkButton ID="ImgbtnFindGroup" runat="server" CssClass="btn btn-primary srearchBtn" Visible="false" OnClick="ImgbtnFindGroup_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    <asp:DropDownList ID="ddlMatGrp" runat="server"
                                        OnSelectedIndexChanged="ddlMatGrp_OnSelectedIndexChanged" AutoPostBack="true" CssClass="form-control chzn-select" TabIndex="3">
                                    </asp:DropDownList>

                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblspecification" runat="server" CssClass="smLbl_to" Text="Specification"></asp:Label>
                                    <asp:DropDownList ID="ddlSpclinf" runat="server" CssClass="form-control chzn-select" TabIndex="3">
                                    </asp:DropDownList>


                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblbill" runat="server" CssClass="smLbl_to" Text="Bill No/Purpose"></asp:Label>
                                    <asp:TextBox ID="txtbillno" runat="server" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                </div>

                                <div class="col-md-1">
                                    <asp:LinkButton ID="lbtnGroupSelect" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnGroupSelect_Click" Style="margin-top: 20px">Select</asp:LinkButton>
                                </div>


                                <div class="form-group" style="display: none">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblResList" runat="server" Font-Size="11px" CssClass="lblName lblTxt" Text="Non Materials List:"></asp:Label>
                                        <asp:TextBox ID="txtResSearch" runat="server" TabIndex="2" CssClass="inputtextbox"></asp:TextBox>

                                        <asp:LinkButton ID="ImgbtnFindRes" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ImgbtnFindRes_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-3 pading5px">
                                        <asp:DropDownList ID="ddlResList" runat="server"
                                            OnSelectedIndexChanged="ddlMatGrp_SelectedIndexChanged" CssClass="form-control inputTxt"
                                            TabIndex="7">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:LinkButton ID="lbtnSelectRes" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnSelectRes_Click">Select</asp:LinkButton>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                            </div>

                        </asp:Panel>

                        <div class="row">
                            <div class="table-responsive">
                                <asp:GridView ID="gvOtherReq" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    Width="542px" ShowFooter="True" OnRowDeleting="gvOtherReq_RowDeleting" OnRowCancelingEdit="gvOtherReq_RowCancelingEdit" OnRowEditing="gvOtherReq_RowEditing" OnRowUpdating="gvOtherReq_RowUpdating" Style="margin: 10px">
                                    <RowStyle />
                                    <Columns>

                                        <asp:CommandField ShowDeleteButton="True" DeleteText="<span class='fa fa-sm fa-trash' style=color:red;'></span>" ControlStyle-CssClass="text-danger" />


                                        <asp:CommandField ShowEditButton="True" />

                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Res Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResCod" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Account Description">

                                            <FooterTemplate>
                                                <table style="width: 21%;">
                                                    <tr>
                                                        <td class="style77">
                                                            <asp:LinkButton ID="lbtnTotal" runat="server" OnClick="lbtnTotal_Click" CssClass="btn  btn-primary primarygrdBtn">Total</asp:LinkButton>
                                                        </td>
                                                        <td class="style65"></td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </FooterTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvactdesc" runat="server"
                                                    Text='<%# "<B>" + Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) + "</B>" +
                                                                         (DataBinder.Eval(Container.DataItem, "sirdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")).Trim(): "")   %>'
                                                    Width="300px">  
                                                
                                                
                                                ></asp:Label>
                                            </ItemTemplate>

                                            <EditItemTemplate>

                                                <fieldset class="scheduler-border fieldset_A">

                                                    <div class="form-horizontal">

                                                        <div class="form-group">

                                                            <div class="col-md-3 pading5px asitCol3">
                                                                <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="lblTxt lblName">Accounts Head</asp:Label>
                                                                <div class="col-md-3 pading5px">

                                                                    <asp:DropDownList ID="ddlgrdacccode" runat="server" CssClass="form-control chzn-select"
                                                                        TabIndex="28" Style="width: 213px;" AutoPostBack="True" OnSelectedIndexChanged="ddlgrdacccode_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </div>



                                                                <%-- <asp:TextBox ID="txtgrdserceacc" runat="server" CssClass="inputtextbox" TabIndex="26"></asp:TextBox>--%>

                                                                <%--<div class="colMdbtn">
                                                    <asp:LinkButton ID="ibtngrdFindAccCode" runat="server" CssClass="btn btn-primary srearchBtn"  TabIndex="27"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                </div>--%>

                                            <

                                           <%-- <div class="col-md-3 pading5px">
                                                

                                            </div--%>>
                                                            </div>
                                                        </div>

                                                        <div class="form-group">

                                                            <div class="col-md-3 pading5px asitCol3">
                                                                <asp:Label ID="lblgvreshead" runat="server" CssClass="lblTxt lblName">Details Head</asp:Label>

                                                                <div class="col-md-3 pading5px">
                                                                    <asp:DropDownList ID="ddlrgrdesuorcecode" runat="server" CssClass="chzn-select"
                                                                        TabIndex="31" Style="width: 213px;">
                                                                    </asp:DropDownList>

                                                                </div>
                                                                <%--<asp:TextBox ID="txtgrdserresource" runat="server" CssClass="inputtextbox" TabIndex="29"></asp:TextBox>--%>


                                                                <%--<div class="colMdbtn">
                                                    <asp:LinkButton ID="ibtngrdFindResource" runat="server" CssClass="btn btn-primary srearchBtn"  TabIndex="30"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                </div>--%>
                                                            </div>


                                                        </div>

                                                    </div>
                                                </fieldset>
                                            </EditItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Bill No/Purpose">

                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvBillno" runat="server" BorderStyle="None"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                    Width="100px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Bgd. Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFBgdamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBgdAmt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0;-#,##0; ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Paid Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPaidAmtxx" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Width="100px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPaidAmtxx" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ppdamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Budget Allocation" Visible="false">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPaidamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPaAmt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Bal. Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBalAmt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFBalamt" runat="server" ForeColor="#000"
                                                    Width="100px" Font-Bold="True" Font-Size="12px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="QTY">

                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvQtamt" runat="server" BackColor="Transparent" Font-Size="11px"
                                                    BorderStyle="None" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />

                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rate">

                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvRate" runat="server" BackColor="Transparent" Font-Size="11px"
                                                    BorderStyle="None" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Width="80px" HorizontalAlign="Right" />

                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Proposed Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFProposedamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Width="100px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvProposedamt" runat="server" BackColor="Transparent" Font-Size="11px"
                                                    BorderStyle="None" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="100px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approved Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFAppamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Width="100px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvApamt" runat="server" BackColor="White" Font-Size="11px"
                                                    BorderStyle="None" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "appamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="100px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>



                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>
                        </div>

                        <asp:Panel ID="pnlNarr" runat="server">
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="Label3" runat="server" CssClass=" smLbl_to" Text="Pay To"></asp:Label>

                                    <asp:TextBox ID="txtPayto" runat="server" class="form-control"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="txtPayto_AutoCompleteExtender"
                                        runat="server" CompletionListCssClass="AutoExtender"
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                        CompletionListItemCssClass="AutoExtenderList" CompletionSetCount="15"
                                        DelimiterCharacters="" Enabled="True" FirstRowSelected="True"
                                        MinimumPrefixLength="0" ServiceMethod="GetRecandPayDetails"
                                        ServicePath="~/AutoCompleted.asmx" TargetControlID="txtPayto">
                                    </cc1:AutoCompleteExtender>
                                </div>

                                <div class="col-md-2" runat="server" id="idattnper" visible="true">
                                    <asp:Label ID="Label2" runat="server" CssClass=" smLbl_to" Text="Attn Person:"></asp:Label>
                                    <asp:TextBox ID="txtAttn" runat="server" class="form-control" placeholder="Enter Person" AutoCompleteType="Disabled"></asp:TextBox>
                                </div>

                                <div class="col-md-2">
                                    <asp:Label runat="server" ID="lblactcode" CssClass="smLbl_to" Text="Account Code(Adjust)"></asp:Label>
                                    <asp:DropDownList ID="ddlactcode" OnSelectedIndexChanged="ddlactcode_OnSelectedIndexChanged" runat="server" CssClass="form-control inputTxt  chzn-select" TabIndex="5" AutoPostBack="True"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label runat="server" ID="lblrescodeadj" CssClass="smLbl_to" Text="Resource Code (Adjust)"></asp:Label>
                                    <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="form-control inputTxt  chzn-select" TabIndex="5"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblpaytype" CssClass=" lblTxt lblName" runat="server">Payment Type:</asp:Label>
                                    <asp:RadioButtonList runat="server" ID="rblpaytype" CssClass="form-control rbtnList1 chkBoxControl" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="Cash">Cash</asp:ListItem>
                                        <asp:ListItem Value="Cheque">Cheque</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>

                                <div class="col-md-2" runat="server" id="bundle" visible="false">
                                    <asp:Label runat="server" ID="lblbundle" CssClass="smLbl_to" Text="Bundle"></asp:Label>
                                    <asp:DropDownList ID="ddlBundle" runat="server" CssClass="form-control ddlPage" TabIndex="3"></asp:DropDownList>
                                </div>
                            </div>

                            <asp:Panel ID="pnlbank" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblBankCode" runat="server" CssClass="lblTxt lblName" Text="Bank Name"></asp:Label>
                                        <asp:TextBox ID="txtserchBankName" Visible="false" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnSrchBank" Visible="false" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnSrchBank_Click" TabIndex="4"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        <asp:DropDownList ID="ddlBankName" runat="server" AutoPostBack="True" CssClass="form-control inputTxt chzn-select"
                                            TabIndex="4" OnSelectedIndexChanged="ddlBankName_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-2">
                                        <asp:Label ID="lblchelist" runat="server" CssClass="lblTxt lblName" Text="Cheque List"></asp:Label>
                                        <asp:DropDownList ID="ddlcheque" runat="server" CssClass="form-control ddlPage chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlcheque_SelectedIndexChanged" TabIndex="4">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="lblRefNum" runat="server" CssClass="smLbl_to" Text="Cheque No"></asp:Label>
                                        <asp:TextBox ID="txtRefNum" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </asp:Panel>



                            <div class="row">
                                <div class="col-md-8">
                                    <asp:Label ID="lblReqNarr" runat="server" CssClass="lblTxt lblName" Text="Narration:"></asp:Label>
                                    <asp:TextBox ID="txtReqNarr" runat="server" class="form-control" Rows="3" TextMode="MultiLine"></asp:TextBox>
                                </div>
                                <asp:LinkButton ID="lbtnUpdateResReq" runat="server" Font-Bold="True" CssClass="btn btn-danger primaryBtn"
                                    Font-Size="12px" ForeColor="White" OnClick="lbtnUpdateResReq_Click" Style="margin-top: 20px">Final Update</asp:LinkButton>
                                <div class="clearfix"></div>
                            </div>

                        </asp:Panel>

                        <asp:Panel ID="pnltermmpay" runat="server">
                            <div class="row">
                                <div class="col-md-8">
                                    <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Text="Terms & Condition:"></asp:Label>

                                    <asp:TextBox ID="termncon" runat="server" class="form-control" Rows="3" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-8 ">
                                    <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName" Text="Mode Of Payment:"></asp:Label>
                                    <asp:TextBox ID="mofpay" runat="server" class="form-control" Rows="3" TextMode="MultiLine" Style="margin-bottom: 20px"></asp:TextBox>
                                </div>
                            </div>

                        </asp:Panel>
                        <div class="clearfix"></div>
                    </div>
                    <div class="card-footer">
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

