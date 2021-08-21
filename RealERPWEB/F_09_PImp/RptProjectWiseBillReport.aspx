
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptProjectWiseBillReport.aspx.cs" Inherits="RealERPWEB.F_09_PImp.RptProjectWiseBillReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

      <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $(".chzn-select").chosen(); 


            var gv = $('#<%=this.gvbillstatus.ClientID %>');
            gv.Scrollable();

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
                                    <div class="col-md-12 pading5px">
                                        <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                        <asp:TextBox ID="txtDateFrom" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDateFrom" Enabled="true"></cc1:CalendarExtender>


                                        <asp:Label ID="lbltoDate" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                        <asp:TextBox ID="txtDateto" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" ToolTip="(dd-MM-yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateto_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDateto" Enabled="true"></cc1:CalendarExtender>


                                        <asp:Label ID="lblPage" runat="server" CssClass="smLbl_to">Page</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" Width="50px"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass="ddlPage">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                        </asp:DropDownList>


                                        


                                      <asp:LinkButton ID="btnok" runat="server" CssClass="btn btn-primary okBtn" OnClick="btnok_Click">Ok</asp:LinkButton>
                 

                                        </div>
                                        

                                        
                                    </div>

                                </div>

                            <div class="form-group">
                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:Label ID="Label17" runat="server" CssClass="lblTxt lblName"
                                            Text="Project Name:"></asp:Label>

                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputtextbox"
                                            TabIndex="3"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:DropDownList ID="ddlProjectName" runat="server"
                                            CssClass="chzn-select ddlistPull" Width="336px" TabIndex="5"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </div>




                                </div>

                                


                                

                        </fieldset>




                    </div>

                 
                        

              
                            <div class="table-responsive table">

                                 
                           <div class="table table-responsive">
                                <asp:GridView ID="gvbillstatus" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" AllowPaging="true" OnPageIndexChanging="gvbillstatus_PageIndexChanging">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                               <Columns>
                               
                                          <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                      
                                          <asp:TemplateField HeaderText="Group Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProjectName0" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                    Width="300px"></asp:Label>
                                            </ItemTemplate>
                                               <FooterTemplate>
                                                <asp:Label ID="lgvFgsirdesc" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="300px"> Total :</asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />

                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        
                                        

                                        <asp:TemplateField HeaderText="Total Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvbilamt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFgvbilamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="90px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="Right" />
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
            <!-- End of Container-->


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

