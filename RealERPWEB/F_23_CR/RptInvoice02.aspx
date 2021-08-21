<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptInvoice02.aspx.cs" Inherits="RealERPWEB.F_23_CR.RptInvoice02" %>
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
                                            CssClass="chzn-select form-control inputTxt" TabIndex="13" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>

                                    
                                </div>
                                <div class="form-group">
                                    <div class="col-md-5 pading5px">
                                        <asp:Label ID="lblFdate" runat="server" CssClass=" lblName lblTxt" Visible="false">From</asp:Label>
                                    <asp:TextBox ID="txFdate"  runat="server" CssClass="inputtextbox " AutoPostBack="true" Visible="false"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender1_txFdate" runat="server" Enabled="True"
                                        Format="dd-MMM-yyyy" TargetControlID="txFdate"></cc1:CalendarExtender>


                                        <asp:Label ID="Label7" runat="server" CssClass="smLbl_to" Text="Date:"> </asp:Label>
                                        <asp:TextBox ID="txtDate" runat="server" AutoCompleteType="Disabled" CssClass=" inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>
                                    </div>

                                </div>
                            </div>

                           
                        <div class="form-horizontal">


                            <div class="form-group">
                                <div class="col-md-6 pading5px ">
                                    <div class="input-group">
                                        <span class="input-group-addon glypingraddon">
                                            <asp:Label ID="Label12" runat="server" CssClass="lblTxt lblName" Text="Subject:" Visible="false"></asp:Label>
                                        </span>
                                        <asp:TextBox ID="txtSubject" runat="server" class="form-control inputTxt" Visible="false" > 
                                        </asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-6 pading5px">
                                    <div class="input-group">
                                        <span class="input-group-addon glypingraddon">
                                            <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Text="Dear Sir," Visible="false"></asp:Label>
                                        </span>

                                    </div>
                                </div>


                            </div>
                            
                            <div class="form-group">
                                <div class="col-md-6 pading5px">
                                    <div class="input-group">
                                        <span class="input-group-addon glypingraddon">
                                            <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName" Text=":"></asp:Label>
                                        </span>
                                        <asp:TextBox ID="txthead" runat="server" TextMode="MultiLine"  class="form-control inputTxt" Visible="false" ></asp:TextBox>
                                    </div>
                                </div>
                                
                            </div>


                        </div>

             

                        </fieldset>
                        

                    </div>


                    <div class="row">
                 
                                                        
                                <asp:GridView ID="gvCustInvoice" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea " OnRowDataBound="gvCustInvoice_RowDataBound">
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
                                        <asp:TemplateField  HeaderText="Items">

                                            <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdateInvoice" runat="server" CssClass="btn btn-danger primaryBtn" OnClientClick="return Confirmation();" OnClick="lbtnUpdateInvoice_Click"> Update</asp:LinkButton>

                                    </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgcResDesc2" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvschamt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0;(#,##0); ") %>'
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
