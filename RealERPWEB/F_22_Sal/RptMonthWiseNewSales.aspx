<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptMonthWiseNewSales.aspx.cs" Inherits="RealERPWEB.F_22_Sal.RptMonthWiseNewSales" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">  
    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {



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

            <div class="card card-fluid">
                <div class=" card-body" style="min-height: 100px;">
                    <div class="row">
                        <div class="col-md-2">
                            <div class=" form-group">
                                <label for="Label11" runat="server" class=" control-label  lblmargin-top9px ">From : </label>
                                <asp:TextBox ID="txtfromdate" runat="server" CssClass="inputDateBox" ToolTip="(dd.mmm.yyyy)"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                            </div>

                        </div>
                        <div class="col-md-2">
                            <div class=" form-group">
                                <label for="Label12" runat="server" class=" control-label  lblmargin-top9px ">To :</label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="inputDateBox" ToolTip="(dd.mmm.yyyy)"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                            </div>

                        </div>

                        <div class="col-md-1">
                             <asp:Label ID="Label5" runat="server"
                                    CssClass="lblTxt lblName" Text="Project Name:"></asp:Label>  
                            </div>
                        <div class="col-md-3">
                             <div class=" form-group">                               
                               <asp:DropDownList ID="ddlProjectName" CssClass="chzn-select form-control  inputTxt" runat="server" Font-Bold="True"
                                    Width="300px">
                                </asp:DropDownList>
                                <cc1:ListSearchExtender ID="ddlProjectName_ListSearchExtender2" runat="server"
                                    QueryPattern="Contains" TargetControlID="ddlProjectName">
                                </cc1:ListSearchExtender>

                             </div>
                                
                            </div>
                      
                        <div class="col-md-1">
                            <div class="from-group">
                                <asp:LinkButton ID="lbtnShow" runat="server" Font-Bold="True" Font-Size="14px" CssClass="btn btn-primary" OnClick="lbtnShow_Click" Style="text-align: center;">Show</asp:LinkButton>
                            </div>

                        </div>

                        
                    </div>
                </div>
            </div>
           <%-- <div class="card card-fluid">--%>
               <%-- <div class="card-body" style="min-height: 100px;">     --%>                  
                            <div class="table table-responsive">
                                <asp:GridView ID="gvNewsales" runat="server" AutoGenerateColumns="False" ShowFooter="True" Width="931px"  CssClass="table-striped table-hover table-bordered  grvContentarea"
                                       OnRowDataBound="gvNewsales_RowDataBound">
                                <%--    <RowStyle BackColor="#D2FFF7" Font-Size="11px" />--%>
                                    <Columns>

                                          <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Style="text-align: right" Font-Size="12px"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                              <ItemStyle HorizontalAlign="right"  Font-Names="Century Gothic" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        
                                          <asp:TemplateField HeaderText="Serial No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblserialno" runat="server" Font-Bold="true" ForeColor="#ff0066" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rowid")) %>'
                                                    Width="40px" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                              <ItemStyle HorizontalAlign="Center" Font-Names="Century Gothic" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Name Of  </br>The Month">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmonthid" runat="server" Font-Bold="true" ForeColor="#ff0066" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "monthid1")) %>'
                                                    Width="100px" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                              <ItemStyle HorizontalAlign="left" Font-Names="Century Gothic" />
                                               <HeaderStyle HorizontalAlign="Left" Font-Names="Century Gothic" />
                                        </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Project code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblprjcode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                    Width="70px" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" Font-Names="Century Gothic" />
                                                 <HeaderStyle HorizontalAlign="Left" Font-Names="Century Gothic" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblprjName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="150px" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" Font-Names="Century Gothic" />
                                             <HeaderStyle HorizontalAlign="Left" Font-Names="Century Gothic" />
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Client Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                                    Width="150px" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                             <ItemStyle HorizontalAlign="left" Font-Names="Century Gothic" />
                                              <HeaderStyle HorizontalAlign="Left" Font-Names="Century Gothic" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Apt. No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvaptno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                    Width="100px" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" Font-Names="Century Gothic" />
                                             <HeaderStyle HorizontalAlign="Left" Font-Names="Century Gothic" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Apt. Size </br> sft">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvusize" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" Font-Names="Century Gothic" />
                                             <HeaderStyle HorizontalAlign="Left" Font-Names="Century Gothic" />
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Sale Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvsaledate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "schdate")) %>'
                                                    Width="80px" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                              <ItemStyle HorizontalAlign="left" Font-Names="Century Gothic" />
                                               <HeaderStyle HorizontalAlign="Left" Font-Names="Century Gothic" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total Sales </br> Value">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvsalesvalue" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsalvalue")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" Font-Names="Century Gothic" />
                                             <HeaderStyle HorizontalAlign="Left" Font-Names="Century Gothic" />
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Total Receive">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvReceive" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "colamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                             <ItemStyle HorizontalAlign="right" Font-Names="Century Gothic" />
                                              <HeaderStyle HorizontalAlign="Left" Font-Names="Century Gothic" />
                                        </asp:TemplateField>

                                        
                                        
                                        
                                        <asp:TemplateField HeaderText="TL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvteam" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "teamlead")) %>'
                                                    Width="150px" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" Font-Names="Century Gothic" />
                                             <HeaderStyle HorizontalAlign="Left" Font-Names="Century Gothic" />
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
                         
                   


            

                  
              <%--  </div>--%>
         <%--   </div>--%>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

