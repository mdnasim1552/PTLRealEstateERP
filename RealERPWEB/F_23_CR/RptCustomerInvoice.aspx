<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptCustomerInvoice.aspx.cs" Inherits="RealERPWEB.F_23_CR.RptCustomerInvoice" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $('.chzn-select').chosen({ search_contains: true });


           <%-- var gv = $('#<%=this.gvSubBill.ClientID %>');
            gv.Scrollable();--%>

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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class=" col-md-3  pading5px asitCol3">

                                        <asp:Label ID="Label5" CssClass="lblTxt lblName" runat="server" Text="Project Name:"></asp:Label>
                                        <asp:TextBox ID="txtSrcProject" runat="server" CssClass=" inputtextbox"></asp:TextBox>


                                        <asp:LinkButton ID="imgbtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindProject_Click" TabIndex="12"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                    </div>


                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control inputTxt chzn-select" TabIndex="13" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <cc1:ListSearchExtender ID="ddlProjectName_ListSearchExtender2" runat="server"
                                            QueryPattern="Contains" TargetControlID="ddlProjectName">
                                        </cc1:ListSearchExtender>
                                    </div>


                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName">Customer Name</asp:Label>
                                        <asp:TextBox ID="txtSrcCustomer" runat="server" CssClass=" inputtextbox" TabIndex="14"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="imgbtnFindCustomer" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindCustomer_Click" TabIndex="15"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>

                                    </div>
                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlCustName" runat="server"
                                            CssClass="form-control inputTxt" TabIndex="13" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-4 pading5px">

                                        <asp:CheckBox ID="chkConsolidate" runat="server" TabIndex="10" Text="Consolidate" Visible="false" CssClass="btn btn-primary checkBox" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName" Text="Date:"> </asp:Label>
                                        <asp:TextBox ID="txtDate" runat="server" AutoCompleteType="Disabled" CssClass=" inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>
                                    </div>

                                </div>
                            </div>
                        </fieldset>

                    </div>
                    <div class="row">
                   
                   

                                <div class="row">

                                    <asp:Label ID="lblPayShe" runat="server" CssClass="lblTxt lblName" Visible="False"></asp:Label>

                                </div>

                                <asp:GridView ID="gvCustInvoice" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea ">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmCode3" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                    Width="49px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField  HeaderText="Particulars">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcResDesc2" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblFpar" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right" Width="100px">Total :</asp:Label>
                                            </FooterTemplate>

                                            <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText=" Schedule Date ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvsDate" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "schdate")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvschamt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lfAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right" Width="100px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        
                                        
                                        
                                        
                                        
                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>

                                

                                


                     
                            
                       
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



