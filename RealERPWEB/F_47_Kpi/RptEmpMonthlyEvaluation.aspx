<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptEmpMonthlyEvaluation.aspx.cs" Inherits="RealERPWEB.F_47_Kpi.RptEmpMonthlyEvaluation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function loadModal() {
            $('#exampleModal3').modal('show');
        }
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });
        }

    </script>
    
                <div class="card card-fluid">
                
                 <div class="card-body">
                    
                    <div class="row">
                        <div class="col-md-1">
                            
                                <div class="form-group">
                                    <label class="control-label   lblmargin-top9px" for="lblfrmdate">Month:</label>

                                </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">

                                  <asp:DropDownList ID="ddlyearmon" runat="server" AutoPostBack="True"
                                    TabIndex="11" CssClass=" form-control ">
                                </asp:DropDownList>
                            </div>

                        </div>

                        <div class="col-md-1">
                            
                                <div class="form-group">
                                    <label class="control-label   lblmargin-top9px" for="lblfrmdate">Name:</label>

                                </div>


                           

                        </div>

                        <div class="col-md-3">
                            <div class="form-group">

                                  <asp:DropDownList ID="ddlTeam" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTeam_SelectedIndexChanged"
                                    TabIndex="11" CssClass=" form-control ">
                                </asp:DropDownList>


                            </div>

                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                    <label class="control-label lblmargin-top9px" for="lblfrmdate">Supervisor Name:</label>

                                </div>
                        </div>
                        <div class="col-md-2" style="margin-top:10px">
                            <div class="form-group">
                                <asp:Label ID="lbbsupname" CssClass="control-label"  runat="server"></asp:Label>
                              </div>
                        </div>

                        <div class="col-md-1">
                           
                                  <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lnkok_Click"   CssClass="btn btn-primary okBtn" TabIndex="9"></asp:LinkButton>
                           
                        </div>

                       
                    </div>




               <asp:GridView ID="gvKRA" runat="server" AllowPaging="True" Width="200px" 
                        AutoGenerateColumns="False" PageSize="15" ShowFooter="true"  OnRowDataBound="gvKRA_RowDataBound"
                        CssClass="table table-striped table-hover table-bordered grvContentarea" OnPageIndexChanging="gvKRA_PageIndexChanging" >
                        <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                            Mode="NumericFirstLast" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="10px"></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="10px" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <FooterTemplate>


                                </FooterTemplate>
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="KRA(Key Result Areas)">

                                <ItemTemplate>
                                    <asp:Label ID="lblgvmgrp" runat="server"
                                        Text='<%#  "<B>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "mkpidesc")) +"</B>"
                                                                       
                                                                         
                                                                    %>'
                                        Width="200px"
                                        Style="text-align: left" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>
                                </ItemTemplate>


                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                                 <asp:TemplateField HeaderText="KPI(Key Performance Indicator)">

                                <ItemTemplate>
                                    <asp:Label ID="lblgvgrp" runat="server"
                                        Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "kpidesc"))
                                                                       
                                                                         
                                                                    %>'
                                        Width="500px"
                                        Style="text-align: left" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>
                                </ItemTemplate>

                               
                                     
                                <FooterTemplate>

                                    
                                   

                                    <asp:LinkButton ID="lnkappupdate" runat="server" CssClass="btn  btn-danger primarygrdBtn"   OnClick="lnkFinalUpdate_Click">Update</asp:LinkButton>


                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Target">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvtarget" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"stdtarget")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None" AutoCompleteType="Disabled"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblFtaroffer" runat="server" Style="text-align: right; color: black;" Width="70px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                          

                       

                             <asp:TemplateField HeaderText="Weight of KPIs">
                                <ItemTemplate>
                                    <asp:Label ID="lblwieght" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"stdkpival")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None" AutoCompleteType="Disabled"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFTotalWeight" runat="server" Style="text-align: right; color: black; font-weight:bold;" ></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="right" />
                                 <FooterStyle HorizontalAlign="Right" />
                             
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="KPI GROUP" Visible="false">
                                 <ItemTemplate>
                                    <asp:Label ID="lblkpigrp" runat="server" BackColor="Transparent"
                                      BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Width="100px"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "kpigrp")) %>'
                                              Height="20px" Font-Size="11px"></asp:Label>
                                  </ItemTemplate>
                             
                             
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>




                          <asp:TemplateField HeaderText="Monthly Achievement">
                                 <ItemTemplate>
                                    <asp:TextBox ID="txtMonthAchieve" runat="server" BackColor="Transparent"  Style="text-align: right"
                                      BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Width="70px"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "achieved")).ToString("#,##0.00;(#,##0.00); ") %>'
                                              Height="20px" Font-Size="11px"></asp:TextBox>
                                  </ItemTemplate>
                             
                             
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>



                        </Columns>
                        <FooterStyle BackColor="#F5F5F5" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                          <RowStyle CssClass="grvRows" />
                        <PagerStyle CssClass="gvPagination" />                       
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>
                  

              <asp:Panel ID="pnlNarration" Visible="false" runat="server">

                <div class="row">
                    <div class="col-md-2">

                        <div class="form-group">
                            <label class="control-label   lblmargin-top9px" for="lblfrmdate">Self Comments:</label>

                        </div>
                    </div>
                    <div class="col-md-6">

                        <div class="form-group">
                            <asp:TextBox ID="txtSelefComment" runat="server" TextMode="MultiLine" Width="800px"></asp:TextBox>

                        </div>
                    </div>
                </div>

                <div class="row">


                    <div class="col-md-2">
                        <div class="form-group">
                            <label class="control-label   lblmargin-top9px" for="lblfrmdate">Supervisor Comments:</label>

                        </div>

                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <asp:TextBox ID="txtSuprvisor" runat="server" TextMode="MultiLine" Width="800px"></asp:TextBox>
                        </div>


                    </div>


                </div>

            </asp:Panel>




                <%--<div class="row">
                    <asp:Label ID="lblSel" runat="server" Text="Self Comments:" CssClass="col-md-1"></asp:Label>
                    <div class="form-group">
                        <asp:TextBox ID="txtSelefComment" runat="server" TextMode="MultiLine" Width="800px" ></asp:TextBox>
                    </div>
                </div>
                 
                    <div class="row">
                    <asp:Label ID="lbl" runat="server" Text="Supervisor Comments:" CssClass="col-md-1"> </asp:Label>

                    <div class="form-group">
                        <asp:TextBox ID="txtSuprvisor" runat="server" TextMode="MultiLine" Width="800px" ></asp:TextBox>
                    </div>
                </div>--%>

         
        </div>
    </div>
    
</asp:Content>


