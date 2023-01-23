<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptPFIndvPay.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_90_PF.RptPFIndvPay" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .mt20 {
            margin-top: 20px;
        }

        .chzn-container {
            width: 100% !important;
        }

        .chzn-drop {
            width: 100% !important;
        }

        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }

        .card-body {
            min-height: 400px !important;
        }

        .pd4 {
            padding: 4px !important;
        }
    </style>



    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {
            var gvswfsum = $('#<%=this.gvswfsum.ClientID %>');
            gvswfsum.Scrollable();

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            $('.chzn-select').chosen({ search_contains: true });
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
            <div class="card mt-5">
                <div class="card-header">
                    <asp:Panel ID="Panel1" runat="server">
                        <div class="row">
                            <div class="col-lg-3">
                                <div class="form-group">
                               <asp:Label ID="Label6" runat="server">Company</asp:Label>

                            <asp:DropDownList ID="ddlCompanyAgg" OnSelectedIndexChanged="ddlCompanyAgg_SelectedIndexChanged" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" TabIndex="2">
                            </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-lg-3">
                                <div class="form-group">
                          <asp:Label ID="lbldeptnameagg" runat="server">Department</asp:Label>
                          
                            <asp:DropDownList ID="ddldepartmentagg" OnSelectedIndexChanged="ddldepartmentagg_SelectedIndexChanged" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" TabIndex="2">
                            </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-lg-3">
                                <div class="form-group">
                              <asp:Label ID="lblsection" runat="server" >Section</asp:Label>
   
                            <asp:DropDownList ID="ddlProjectName" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" runat="server" CssClass="form-control form-control-sm chzn-select  " AutoPostBack="true" TabIndex="2">
                            </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-lg-2">
                                <div class="form-group">
                              <asp:Label ID="Label2" runat="server" >Employee List:</asp:Label>
                            <asp:DropDownList ID="ddlEmpNameAllInfo" runat="server" CssClass="form-control form-control-sm chzn-select" TabIndex="2"  OnSelectedIndexChanged="ddlEmpNameAllInfo_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-lg-1">
                            <asp:LinkButton ID="lnkbtnSerOk" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lnkbtnSerOk_Click">Ok</asp:LinkButton>

                            </div>
                        </div>

                        <div class="from-group">
                                   <div class="col-md-2">                           
                                <label class="control-label" for="ToDate">Date</label>                           
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm" autocomplete="off"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                           
                        </div>
                        </div>
                          
                    </asp:Panel>
                </div>
                <div class="card-body">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="View1" runat="server"> 
                       <div class="row table-responsive">

                <asp:GridView ID="gvpayinfo" runat="server" AutoGenerateColumns="False"
                    ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                    <RowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="SL">
                            <ItemTemplate>
                                <asp:Label ID="serialno" runat="server" Style="text-align: right"
                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Payment Date">
                            <ItemTemplate>
                                <asp:Label ID="lblgvDate" runat="server" Font-Size="11PX"
                                    Style="text-align: left"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydate")) %>'
                                    Width="40px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Present Salary">
                            <ItemTemplate>
                                <asp:Label ID="lblgvIncSalary" runat="server" Font-Size="11PX" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salary")).ToString("#, ##0.00;(#, ##0.00); ") %>'
                                    Width="60px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblfoopf" runat="server" Font-Size="11PX" Font-Bold="true" Style="text-align: right" Text="Grand Total :" Width="80px"></asp:Label>
                            </FooterTemplate>
                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="P.F Amount">
                            <ItemTemplate>
                                <asp:Label ID="lblgvSalary" runat="server" Font-Size="11PX"
                                    Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pfamt")).ToString("#, ##0.00;(#, ##0.00); ") %>'
                                    Width="60px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lbltotalpf" runat="server" Font-Size="11PX" Font-Bold="true" Style="text-align: right" Width="80px"></asp:Label>
                            </FooterTemplate>
                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="P.F </br> Contribution">
                            <ItemTemplate>
                                <asp:Label ID="lblgvContri" runat="server" Font-Size="11PX"
                                    Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "contribu")).ToString("#, ##0.00;(#, ##0.00); ") %>'
                                    Width="60px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="gvFcontribu" runat="server" Font-Size="11PX" Font-Bold="true" Style="text-align: right" Width="80px"></asp:Label>
                            </FooterTemplate>
                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Balance">
                            <ItemTemplate>
                                <asp:Label ID="lblgvSalary" runat="server" Font-Size="11PX"
                                    Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balance")).ToString("#, ##0.00;(#, ##0.00); ") %>'
                                    Width="60px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="gvFBalance" runat="server" Font-Size="11PX" Font-Bold="true" Style="text-align: right" Width="80px" Visible="false"></asp:Label>
                            </FooterTemplate>
                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateField>

                    </Columns>
                    <FooterStyle CssClass="grvFooter" />
                    <EditRowStyle />
                    <AlternatingRowStyle />
                    <PagerStyle CssClass="gvPagination" />
                    <HeaderStyle CssClass="grvHeader" />
                </asp:GridView>
            </div>
                            </asp:View>


                    <asp:View ID="Viewswfsum" runat="server"> 
       
                <asp:GridView ID="gvswfsum" runat="server" AutoGenerateColumns="False"
                    ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                    <RowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="SL">
                            <ItemTemplate>
                                <asp:Label ID="serialnoswf" runat="server" Style="text-align: right"
                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Month ">
                            <ItemTemplate>
                                <asp:Label ID="lblgvmonth" runat="server" Font-Size="11PX"
                                    Style="text-align: left"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydate")) %>'
                                    Width="100px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True"  />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Swf Amount">
                            <ItemTemplate>
                                <asp:Label ID="lblgvswfamt" runat="server" Font-Size="11PX"
                                    Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "swf")).ToString("#, ##0.00;(#, ##0.00); ") %>'
                                    Width="60px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="gvFgvswfamt" runat="server" Font-Size="11PX" Font-Bold="true" Style="text-align: right" Width="80px"></asp:Label>
                            </FooterTemplate>
                            <HeaderStyle Font-Bold="True" />
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="P.F Amount">
                            <ItemTemplate>
                                <asp:Label ID="lblgvpfswf" runat="server" Font-Size="11PX"
                                    Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pfamt")).ToString("#, ##0.00;(#, ##0.00); ") %>'
                                    Width="60px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblFgvpfswf" runat="server" Font-Size="11PX" Font-Bold="true" Style="text-align: right" Width="80px"></asp:Label>
                            </FooterTemplate>
                            <HeaderStyle Font-Bold="True" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>

                        


                        <asp:TemplateField HeaderText="Total Amount">
                            <ItemTemplate>
                                <asp:Label ID="lblgvtotalamt" runat="server" Font-Size="11PX"
                                    Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalamt")).ToString("#, ##0.00;(#, ##0.00); ") %>'
                                    Width="60px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="gvFtotamat" runat="server" Font-Size="11PX" Font-Bold="true" Style="text-align: right" Width="80px"></asp:Label>
                            </FooterTemplate>
                            <HeaderStyle Font-Bold="True" />
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateField>

                    </Columns>
                    <FooterStyle CssClass="grvFooter" />
                    <EditRowStyle />
                    <AlternatingRowStyle />
                    <PagerStyle CssClass="gvPagination" />
                    <HeaderStyle CssClass="grvHeader" />
                </asp:GridView>
         
              </asp:View>

                         <asp:View ID="View2" runat="server"> 
       
                <asp:GridView ID="gvpfSattlement" runat="server" AutoGenerateColumns="False"
                    ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                    <RowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="SL">
                            <ItemTemplate>
                                <asp:Label ID="serialnosattle" runat="server" Style="text-align: right"
                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Year ">
                            <ItemTemplate>
                                <asp:Label ID="lblgvyearid" runat="server" Font-Size="11PX"
                                    Style="text-align: left"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "yearid")) %>'
                                    Width="100px"></asp:Label>
                            </ItemTemplate>

                            <FooterTemplate>
                                <asp:Label ID="lblFgvyear" runat="server" Font-Size="11PX" Font-Bold="true" Style="text-align: right" Width="100px">Total :</asp:Label>
                            </FooterTemplate>
                            <HeaderStyle Font-Bold="True"  />
                        <ItemStyle HorizontalAlign="left" Font-Bold="true" />
                         <FooterStyle HorizontalAlign="Right" />


                        </asp:TemplateField>

                        
                        
                        <asp:TemplateField HeaderText="PF">
                            <ItemTemplate>
                                <asp:Label ID="lblgvpfsattle" runat="server" Font-Size="11PX"
                                    Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pfamt")).ToString("#, ##0.00;(#, ##0.00); ") %>'
                                    Width="70px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblFgvpfsattle" runat="server" Font-Size="11PX" Font-Bold="true" Style="text-align: right" Width="80px"></asp:Label>
                            </FooterTemplate>
                            <HeaderStyle Font-Bold="True" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="SWF">
                            <ItemTemplate>
                                <asp:Label ID="lblgvswfamtsattle" runat="server" Font-Size="11PX"
                                    Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "swf")).ToString("#, ##0.00;(#, ##0.00); ") %>'
                                    Width="70px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="gvFgvswfamtsattle" runat="server" Font-Size="11PX" Font-Bold="true" Style="text-align: right" Width="80px"></asp:Label>
                            </FooterTemplate>
                            <HeaderStyle Font-Bold="True" />
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateField>

                        


                        <asp:TemplateField HeaderText="Total">
                            <ItemTemplate>
                                <asp:Label ID="lblgvtotalamtsattle" runat="server" Font-Size="11PX"
                                    Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalamt")).ToString("#, ##0.00;(#, ##0.00); ") %>'
                                    Width="70px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="gvFtotamatsattle" runat="server" Font-Size="11PX" Font-Bold="true" Style="text-align: right" Width="80px"></asp:Label>
                            </FooterTemplate>
                            <HeaderStyle Font-Bold="True" />
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateField>

                    </Columns>
                    <FooterStyle CssClass="grvFooter" />
                    <EditRowStyle />
                    <AlternatingRowStyle />
                    <PagerStyle CssClass="gvPagination" />
                    <HeaderStyle CssClass="grvHeader" />
                </asp:GridView>
         
              </asp:View>


                    </asp:MultiView>
            

                </div>
            </div>




          <%--  <fieldset class="scheduler-border fieldset_A">
                <div class="form-horizontal">
                    <div class="form-group">
                        <div class="col-md-3 pading5px asitCol3">
                            <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                            <asp:TextBox ID="txtSrcCompanyAgg" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                            <asp:LinkButton ID="ibtnFindCompanyAgg" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ibtnFindCompanyAgg_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                        </div>
                        <div class="col-md-4 pading5px">
                            <asp:DropDownList ID="ddlCompanyAgg" OnSelectedIndexChanged="ddlCompanyAgg_SelectedIndexChanged" runat="server" CssClass="form-control inputTxt" AutoPostBack="true" TabIndex="2">
                            </asp:DropDownList>
                            <asp:Label ID="lblCompanyNameAgg" runat="server" Style="border: none; line-height: 1.5" CssClass="form-control dataLblview" Height="22" Visible="false"></asp:Label>
                        </div>
                        <div class="colMid">
                            <asp:LinkButton ID="lnkbtnSerOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkbtnSerOk_Click">Ok</asp:LinkButton>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-3 pading5px asitCol3">
                            <asp:Label ID="lbldeptnameagg" runat="server" CssClass="lblTxt lblName">Department</asp:Label>
                            <asp:TextBox ID="txtsrchdeptagg" runat="server" CssClass="inputTxt inputName inpPixedWidth" OnTextChanged="txtsrchdeptagg_TextChanged"></asp:TextBox>
                            <asp:LinkButton ID="lbtndeptagg" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="lbtndeptagg_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                        </div>
                        <div class="col-md-4 pading5px">
                            <asp:DropDownList ID="ddldepartmentagg" OnSelectedIndexChanged="ddldepartmentagg_SelectedIndexChanged" runat="server" CssClass="form-control inputTxt" AutoPostBack="true" TabIndex="2">
                            </asp:DropDownList>
                            <asp:Label ID="lblvaldeptagg" runat="server" CssClass="form-control dataLblview" Height="22" Style="border: none; line-height: 1.5" Visible="false"></asp:Label>
                        </div>



                    </div>
                    <div class="form-group">
                        <div class="col-md-3 pading5px asitCol3">
                            <asp:Label ID="lblsection" runat="server" CssClass="lblTxt lblName">Section</asp:Label>
                            <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                            <asp:LinkButton ID="ibtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ibtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                        </div>
                        <div class="col-md-4 pading5px">
                            <asp:DropDownList ID="ddlProjectName" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" runat="server" CssClass="chzn-select form-control inputTxt" AutoPostBack="true" TabIndex="2">
                            </asp:DropDownList>
                            <asp:Label ID="lblProjectdesc" runat="server" CssClass="form-control dataLblview" Height="22" Style="border: none; line-height: 1.5" Visible="false"></asp:Label>
                        </div>

                        <div class="col-md-3 pading5px asitCol3 pull-right">
                        </div>

                    </div>

                    <div class="form-group">
                        <div class="col-md-3 pading5px asitCol3">
                            <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Employee List:</asp:Label>
                            <asp:TextBox ID="txtEmpSrcInfo" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                            <asp:LinkButton ID="ibtnEmpListAllinfo" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ibtnEmpListAllinfo_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                        </div>
                        <div class="col-md-3 pading5px asitCol3">
                            <asp:DropDownList ID="ddlEmpNameAllInfo" runat="server" CssClass="form-control inputTxt" TabIndex="2" Width="385px" OnSelectedIndexChanged="ddlEmpNameAllInfo_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>


                    </div>
                </div>
            </fieldset>--%>





        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



