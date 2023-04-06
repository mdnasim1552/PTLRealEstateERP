<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="PurConWrkOrderEntry.aspx.cs" Inherits="RealERPWEB.F_09_PImp.PurConWrkOrderEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

        }

    </script>
    <style type="text/css">
        .mt20 {
            margin-top: 20px !important;
        }

        .chzn-drop {
            width: 100% !important;
        }

        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }

        .chzn-container {
            width: 100% !important;
        }

        .lineheight {
        }

        .customchk {
            padding: 2px;
            margin-left: 20px;
        }

            .customchk label {
                padding-left: 5px;
            }

        .moduleItemWrpper .btn-info {
            background-color: #5bc0de;
            color: #000;
            font-weight: bold;
            border: 1px solid #155273 !important;
            display: inline-table !important;
        }
    </style>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

        }

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

            <div class="card mt-4">
                <div class="card-header">
                    <div class="form-group row">
                        <div class="col-md-2 ">
                            <asp:Label ID="lblCurNo" runat="server" CssClass="lblTxt lblName" Text="Order No:"></asp:Label>
                            <div class="row">
                                <asp:Label ID="lblCurISSNo1" runat="server" CssClass="form-control form-control-sm col-6"></asp:Label>
                                <asp:TextBox ID="txtCurISSNo2" runat="server" CssClass="form-control form-control-sm col-6">000</asp:TextBox>
                            </div>

                        </div>
                        <div class="col-lg-1 ">

                            <asp:Label ID="Label7" runat="server" CssClass=" smLbl_to" Text="Date"></asp:Label>
                            <asp:TextBox ID="txtCurISSDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtCurISSDate_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtCurISSDate"></cc1:CalendarExtender>
                        </div>
                        <div class="col-lg-1 ">
                            <asp:Label ID="lblorderref" runat="server" Text="SMCR.No."></asp:Label>
                            <asp:TextBox ID="txtOrderRef" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                        </div>



                        <div class="col-lg-2">
                            <asp:Label ID="lblContractorList" runat="server" Text="Contractor Name"></asp:Label>
                            <asp:DropDownList ID="ddlContractorlist" runat="server" CssClass="chzn-select form-control form-control-sm " AutoPostBack="True" TabIndex="3"></asp:DropDownList>
                        </div>

                        <div class="col-md-2">
                            <asp:Label ID="Label4" runat="server" Text="Project Name"></asp:Label>

                            <asp:DropDownList ID="ddlprjlist" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="True"></asp:DropDownList>
                        </div>



                        <div class="col-md-2">
                            <asp:LinkButton ID="lbtnPrevList" runat="server" CssClass="lblTxt lblName prvLinkBtn" OnClick="lbtnPrevList_Click">Prev. List:</asp:LinkButton>
                            <asp:DropDownList ID="ddlPrevList" runat="server" CssClass=" form-control form-control-sm chzn-select" AutoPostBack="True"></asp:DropDownList>

                        </div>
                        <div class="col-md-1 mt-4">
                            <asp:CheckBox runat="server" ID="Checkrate" Text="ProposeRate"/>
                        </div>

                        <div class="col-lg-1">
                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lbtnOk_Click" TabIndex="11">Ok</asp:LinkButton>

                        </div>


                    </div>
                </div>
            </div>








            <div class="card">
                <asp:Panel ID="PnlRes" runat="server" Visible="False">
                    <div class="card-header">

                        <div class="form-group row">

                            <div class="col-lg-2">
                                <asp:Label ID="lblfloorno" runat="server" CssClass="lblTxt lblName" Text="Floor No"></asp:Label>
                                <asp:DropDownList ID="ddlfloorno" runat="server" CssClass=" form-control form-control-sm" OnSelectedIndexChanged="ddlfloorno_SelectedIndexChanged" TabIndex="12" AutoPostBack="True">
                                    <asp:ListItem Selected="True" Text="Unspecified" Value="00"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-lg-1">
                                <asp:Label ID="lblLabour" runat="server" Text="">Labour     
                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="ibtnSearchMaterisl_Click" TabIndex="2"><i class="fa fa-search "> </i></asp:LinkButton>
                                </asp:Label>
                                <asp:TextBox ID="txtSearchLabour" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>


                            </div>
                            <div class="col-lg-2 mt20">
                                <cc1:DropCheck ID="DropCheck1" runat="server" BackColor="Black" CssClass="mt20"
                                    MaxDropDownHeight="200" TabIndex="8" TransitionalMode="True">
                                </cc1:DropCheck>
                            </div>


                            <%-- <asp:DropDownList ID="ddllabour" runat="server" CssClass=" form-control inputTxt" TabIndex="16" >
                                                </asp:DropDownList>--%>

                            <div class="col-md-1">

                                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="lbtnSelect_Click" CssClass="btn btn-primary btn-sm mt20"
                                    TabIndex="17">Select</asp:LinkButton>
                            </div>
                            <div class="col-md-1">

                                <asp:Label ID="lblPage" runat="server" Text="Page"></asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" TabIndex="18">
                                    <asp:ListItem Value="15">15</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                    <asp:ListItem Value="150">150</asp:ListItem>
                                    <asp:ListItem Value="200">200</asp:ListItem>
                                    <asp:ListItem Value="300">300</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>




                    </div>
                </asp:Panel>
                <div class="card-body">
                    <div class="table-responsive">
                        <asp:GridView ID="grvissue" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" ShowFooter="True" Width="599px" CssClass="table-striped table-hover table-bordered grvContentarea"
                            OnRowDeleting="grvissue_RowDeleting" OnPageIndexChanging="grvissue_PageIndexChanging" PageSize="15">
                            <PagerSettings Position="TopAndBottom" />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMSRSlNo" runat="server" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" />
                                <asp:TemplateField HeaderText="Item Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblitemcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Floor Desc.">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkTotal" runat="server" CssClass="btn btn-primary primaryBtn"
                                            OnClick="lnkTotal_Click">Total</asp:LinkButton>
                                    </FooterTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblFloordes" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>' Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="Spec">

                                        <ItemTemplate>
                                            <asp:TextBox ID="txtspec" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spec")) %>' Width="100px" BorderStyle="None" TextMode="MultiLine"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Description">

                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkupdate" runat="server" CssClass="btn btn-danger primaryBtn"
                                            OnClick="lnkupdate_Click">Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblwrkdesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                            Width="200px"></asp:Label>
                                        <asp:TextBox ID="txtwrkdesc" BackColor="Transparent" BorderStyle="None" runat="server" TextMode="MultiLine" Height="50px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sdetails")).Trim() %>'
                                            Width="500px">
                                            <%--sdetails--%>
                                        </asp:TextBox>

                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="Label14" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px" BackColor="Transparent" Style="text-align: right" BorderStyle="None"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFQty" runat="server" Style="text-align: right"
                                            Width="70px" Font-Size="12px" ForeColor="#000"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvrate" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px" BackColor="Transparent" Style="text-align: right" BorderStyle="None"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvAmount" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px" BackColor="Transparent" Style="text-align: right" BorderStyle="None"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFamount" runat="server" Style="text-align: right"
                                            Width="80px" Font-Size="12px" ForeColor="#000"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtisurmk" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rmrks")) %>'
                                            Width="150px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>
                    </div>
                </div>


            </div>


            <asp:Panel ID="PnlNarration" runat="server" Visible="False">


                <div class="form-group row">
                    <div class="col-md-3">
                               <asp:Label ID="Label1" runat="server"  Text="Date Of Commencement Of Work:"></asp:Label>

                    <asp:TextBox ID="txtcomncdat" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtcomncdat"></cc1:CalendarExtender>
                    </div>

                    <div class="col-md-3 ">
                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt" Text="Date Of Completion:"></asp:Label>
                                           <asp:TextBox ID="txtcompltdat" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server"
                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtcompltdat"></cc1:CalendarExtender>

                    </div>

                    <div class="col-md-3 mt20">
                        
                    <asp:CheckBox ID="ChkLanguage" runat="server" AutoPostBack="True" Text="Terms & Conditions Bangla" CssClass="mt20"
                        Visible="False" />
                    </div>
                </div>
                <div class="form-group row">
    
      
    
                                <asp:Label ID="Label12" runat="server" CssClass="col-2" Text="Subject:"></asp:Label>
                      
                            <asp:TextBox ID="txtSubject" runat="server" class="form-control form-control-sm col-10">Work Order For Materials</asp:TextBox>
           
       
                </div>
                <div class="form-group row">
                    <div class="col-md-6 ">
      
                                <asp:Label ID="Label5" runat="server" Text="Dear Concern,"></asp:Label>
                
                    </div>
                </div>
                <div class="form-group row">
             
           
                                <asp:Label ID="Label3" runat="server" CssClass="col-lg-2 " Text=":"></asp:Label>
                      
                            <asp:TextBox ID="txtLETDES" runat="server" class="form-control col-10 inputTxt" TextMode="MultiLine" Style="height: 150px; line-height: 18px;"> </asp:TextBox>
                 
              
                </div>
                <div class="form-group row">
                                <asp:Label ID="lblNaration" runat="server" CssClass="col-2 " Text="Term & Condition:"></asp:Label>
                            <asp:TextBox ID="txtTerm" runat="server" class="form-control col-10" Rows="2" TextMode="MultiLine" Style="height: 300px; line-height: 18px;"></asp:TextBox>
         
                </div>

                <div class="form-group row">

                                <asp:Label ID="lblPayTerms" runat="server" CssClass="col-2" Text="Payment Terms:"></asp:Label>
                            <asp:TextBox ID="txtPayTerm" runat="server" class="form-control col-10" Rows="2" TextMode="MultiLine" Style="height: 300px; line-height: 18px;"></asp:TextBox>
                
                </div>

            </asp:Panel>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

