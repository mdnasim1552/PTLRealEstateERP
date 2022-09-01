<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptDuesReportAll.aspx.cs" Inherits="RealERPWEB.F_23_CR.RptDuesReportAll" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        $(document).ready(function () {
            //alert("I m IN");
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {


            try {
               
               // gvDues.Scrollable();
                
                var gvDues = $('#<%=this.gvDues.ClientID %>');
                gvDues.Scrollable();
                $('.chzn-select').chosen({ search_contains: true });

            }

            catch (e) {

                alert(e.message)
            }


        }

    </script>
  




    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress9" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
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
            <div class=" card card-fluid">
                <div class=" card-body">                                                                                                                                                        
                            <div class="card card-fluid">
                                <div class="card-body">
                                    <div class="row">

                                        <%--<div class="col-md-1">
                                            <div class="form-group">
                                                
                                            </div>
                                        </div>--%>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Label ID="Label13" runat="server" CssClass="control-label lblmargin-top9px" Text="Date :" Font-Bold="true"></asp:Label>
                                                <asp:TextBox ID="txtAsDate" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender5" runat="server"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtAsDate" Enabled="true"></cc1:CalendarExtender>


                                            </div>
                                        </div>
                                 
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label15" runat="server" CssClass="control-label lblmargin-top9px" Text="Project Name :" Font-Bold="true"></asp:Label>
                                                <asp:DropDownList ID="ddlProjectcode" runat="server" CssClass="custom-select chzn-select" TabIndex="6" AutoPostBack="true">
                                                   
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:LinkButton ID="lnkOk" runat="server" CssClass="btn btn-primary" style="margin-top:20px" 
                                                    OnClick="lnkOk_Click">Ok</asp:LinkButton>

                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                           <asp:Label ID="Label1" runat="server" CssClass="control-label lblmargin-top9px" Text="Dues Type :" Font-Bold="true"></asp:Label>
                                            <div class="form-group">

                                                <asp:DropDownList ID="ddlreportType" runat="server" CssClass="custom-select" TabIndex="6"  AutoPostBack="true" >
                                                    <asp:ListItem Value="TodayDues"> Today Dues</asp:ListItem>
                                                    <asp:ListItem Value="Current" Selected="True">Current Month Dues</asp:ListItem>                                            
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                   <asp:RadioButtonList ID="rbtntype1" RepeatDirection="Horizontal" CssClass=""  runat="server">                                   
                                   <asp:ListItem Value ="Booking">Booking Dues</asp:ListItem>
                                  <asp:ListItem Value="CRDUES"> CR Dues</asp:ListItem>                               
                                  <asp:ListItem Selected="True">ALL Dues</asp:ListItem>
                                </asp:RadioButtonList>                                     
                                    </div>
                                    <div class="row">
                                        <asp:GridView ID="gvDues" runat="server" AutoGenerateColumns="False"
                                            CssClass=" table-striped table-hover table-bordered grvContentarea gvTopHeader"
                                            PageSize="20" ShowFooter="True">
                                            <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblserialnoidas" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="33px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code" visible="false" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcodeas" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>' Width="95px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="Project Name">

                                        <HeaderTemplate>

                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                Text="Project Name" Width="250px"></asp:Label>


                                            <asp:HyperLink ID="hlbtntbCdataExelas" runat="server"
                                                CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><span class="fa  fa-file-excel"></span></asp:HyperLink>

                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HLgvDescas" runat="server"
                                                Font-Size="12px" Font-Underline="False" Target="_blank"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="250px"></asp:HyperLink>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="left" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    
                                    <asp:TemplateField HeaderText="Customer Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvAcDescas" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>' Width="180px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvUDescas" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Unitname")) %>' Width="120px"></asp:Label>
                                        </ItemTemplate>
                                             <FooterTemplate>
                                            <asp:Label ID="lblgvFUDescas" runat="server" Font-Bold="True"></asp:Label>
                                        </FooterTemplate>
                                         <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    
                                    <asp:TemplateField HeaderText="Installment">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvInstallment" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>' Width="120px"></asp:Label>
                                        </ItemTemplate>
                                             <FooterTemplate>
                                            <asp:Label ID="lblgvFInstallment" runat="server" Font-Bold="True"> </asp:Label>
                                        </FooterTemplate>
                                         <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Schedule Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvschdat" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "schdat")) %>' Width="70px"></asp:Label>
                                        </ItemTemplate>
                                             <FooterTemplate>
                                            <asp:Label ID="lblgvFschdat" runat="server" Font-Bold="True"> Total :</asp:Label>
                                        </FooterTemplate>
                                         <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                          

                                      <asp:TemplateField HeaderText="Prev.</br> Booking Dues" Visible="false">
                                        <FooterTemplate>
                                            <asp:Label ID="lblFprbookdues" runat="server" Font-Bold="True"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprbookdues" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pbookam")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Prev.</br> CR Dues" Visible="false">
                                        <FooterTemplate>
                                            <asp:Label ID="lbplFCrdues" runat="server" Font-Bold="True"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprCrdues" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pinsam")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText=" Current.</br>Booking Dues" Visible="false">
                                        <FooterTemplate>
                                            <asp:Label ID="lblfcbookdues" runat="server" Font-Bold="True"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcbookdues" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cbookam")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Current.</br> CR Dues" Visible="false" >
                                        <FooterTemplate>
                                            <asp:Label ID="lblfcurrcrdues" runat="server" Font-Bold="True"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcurrcrdues" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cinsam")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText=" Total Dues">
                                        <FooterTemplate>
                                            <asp:Label ID="lblFtotaldues" runat="server" Font-Bold="True"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtotaldues" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "todues")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                            <RowStyle CssClass="grvRows" />
                                        </asp:GridView>
                                    </div>


                                </div>
                            </div>
                    


                        
                        

                        
                    

                        
                        
                        


                </div>
                <!-- End of contentpart-->
            </div>
            <!-- End of Container-->
            <asp:MultiView ID="MultiViewold" runat="server">
            </asp:MultiView>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

