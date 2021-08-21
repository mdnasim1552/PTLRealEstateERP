<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="EmpStdKpi02.aspx.cs" Inherits="RealERPWEB.F_47_Kpi.EmpStdKpi02" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  
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
    

<%--    <div class="container moduleItemWrpper">
        <div class="contentPart">
            <div class="row">--%>




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
                                    <label class="control-label   lblmargin-top9px" for="lblgroup">Group</label>

                                </div>


                           

                        </div>
                        <div class="col-md-4">
                            
                                <div class="form-group">


                                  
                                      <asp:DropDownList ID="ddlgroup" runat="server" CssClass="  form-control"
                                   >
                                    <asp:ListItem Value="0301001">Assistant Executive, Sales to Deputy Manager</asp:ListItem>
                                    <asp:ListItem Value="0301002">Team Leader / Branch Head / Cluster Head</asp:ListItem>
                                   
                                </asp:DropDownList>


                                </div>


                           

                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                  <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lnkok_Click" CssClass="btn btn-primary okBtn" TabIndex="9"></asp:LinkButton>
                            </div>
                        </div>

                       
                    </div>




               
                    <asp:GridView ID="gvStdKpi" runat="server" AllowPaging="True" Width="200px" OnRowDeleting="gvStdKpi_RowDeleting"
                        AutoGenerateColumns="False" PageSize="15" ShowFooter="true"
                        CssClass="table table-striped table-hover table-bordered grvContentarea" OnPageIndexChanging="gvStdKpi_PageIndexChanging">
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

                                <FooterTemplate>

                                    
                                    <asp:LinkButton ID="lnkTotal" runat="server" CssClass="btn btn-primary primarygrdBtn" OnClick="lnkTotal_Click">Total</asp:LinkButton>

                                    <asp:LinkButton ID="lnkappupdate" runat="server" CssClass="btn  btn-danger primarygrdBtn" OnClick="lnkappupdate_Click">Update</asp:LinkButton>


                                </FooterTemplate>

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

                               

                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Target">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvtarget" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"stdtarget")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None" AutoCompleteType="Disabled"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblFtaroffer" runat="server" Style="text-align: right; color: black;" Width="70px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                          

                       

                             <asp:TemplateField HeaderText="Weight of KPIs">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvwieght" runat="server"
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

                          






                        </Columns>
                        <FooterStyle BackColor="#F5F5F5" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                          <RowStyle CssClass="grvRows" />
                        <PagerStyle CssClass="gvPagination" />                       
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>

               


                <div class="modal fade AsitModal" id="exampleModal3" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog row">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close btn-danger" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>

                                <h3 class="modal-title" id="myModalLabel">Employee KPI Group Setup</h3>


                            </div>
                            <div class="modal-body">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">

                                    <ContentTemplate>


                                        <fieldset class="scheduler-border fieldset_B">

                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <div class="col-md-3 pading5px asitCol2">
                                                        <asp:LinkButton ID="btnComp" runat="server" OnClick="btnComp_Click" ToolTip="Click for Search" TabIndex="4" CssClass="lblTxt lblName">Company Name:</asp:LinkButton>

                                                        <asp:TextBox ID="txtComp" runat="server" TabIndex="3" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>

                                                    </div>



                                                    <div class="col-md-3 pading5px asitCol2">
                                                        <asp:DropDownList ID="ddlCompName" runat="server" AutoPostBack="True" CssClass="ddlPage235 inputTxt"
                                                            TabIndex="5">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-1 asitColPad">
                                                        <asp:LinkButton ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" CssClass="btn btn-primary okBtn" TabIndex="9"></asp:LinkButton>


                                                    </div>
                                                    <div class="clearfix"></div>


                                                </div>
                                                <div class="form-group">
                                                    <div class="col-md-3 pading5px asitCol2">

                                                        <asp:LinkButton ID="btnDpt" runat="server" OnClick="btnDpt_Click" ToolTip="Click for Search" TabIndex="4" CssClass="lblTxt lblName">Dept Name:</asp:LinkButton>

                                                        <asp:TextBox ID="txtDpt" runat="server" TabIndex="3" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>

                                                    </div>



                                                    <div class="col-md-3 pading5px asitCol2">
                                                        <asp:DropDownList ID="ddlDptcode" runat="server" AutoPostBack="True" CssClass="ddlPage235 inputTxt"
                                                            TabIndex="5">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-3 pading5px asitCol2">
                                                        <div class="msgHandSt">
                                                            <asp:Label ID="lblmsg01" runat="server" CssClass="btn-danger btn disabled" Visible="false"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="lblgrp" runat="server" Visible="false"></asp:Label>
                                                    </div>
                                                    <div class="clearfix"></div>


                                                </div>
                                                <%--<div class="form-group">
                                                    <div class="col-md-3 pading5px asitCol2">

                                                        <asp:LinkButton ID="btnSircode" runat="server" OnClick="btnSircode_Click" ToolTip="Click for Search" TabIndex="4" CssClass="lblTxt lblName">Activity Name:</asp:LinkButton>

                                                        <asp:TextBox ID="txtSircode" runat="server" TabIndex="3" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>

                                                    </div>



                                                    <div class="col-md-3 pading5px asitCol2">
                                                        <asp:DropDownList ID="ddlSircode" runat="server" AutoPostBack="True" CssClass="form-control inputTxt"
                                                            TabIndex="5">
                                                        </asp:DropDownList>
                                                    </div>
                                                    
                                                    <div class="clearfix"></div>


                                                </div>--%>
                                        </fieldset>






                                        <div>
                                            <asp:GridView ID="gvKpiGrp" runat="server" AllowPaging="false" Width="200px"
                                                AutoGenerateColumns="False" ShowFooter="true" OnRowDeleting="gvKpiGrp_RowDeleting"
                                                CssClass="table table-striped table-hover table-bordered grvContentarea">
                                                <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                                    Mode="NumericFirstLast" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="10px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ControlStyle Width="10px" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:CommandField ShowDeleteButton="True" />

                                                    <asp:TemplateField HeaderText="Empid" Visible="false">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvEmpid" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"empid")) %>'
                                                                Width="100px"
                                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee Name">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvEmpname" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"empname")) %>'
                                                                Width="140px"
                                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="KPI Grp">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvGrp" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"kpigrp")) %>'
                                                                Width="80px"
                                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Company Name">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvComdesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"comnam")) %>'
                                                                Width="140px"
                                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Wrkdept" Visible="false">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvwrkdpt" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"wrkdpt")) %>'
                                                                Width="100px"
                                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Dept Desc">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDptdesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"wrkdptdesc")) %>'
                                                                Width="180px"
                                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="lUpdatModInfo" runat="server" OnClick="lUpdatModInfo_Click" CssClass="btn btn-danger primaryBtn">Final Update</asp:LinkButton>

                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#F5F5F5" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                            </asp:GridView>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                <%--<asp:LinkButton ID="lnkClose" runat="server" CssClass="btn btn-default" OnClick="lnkClose_Click">Close</asp:LinkButton>--%>
                                <%--<button type="button" class="btn btn-primary">Save changes</button>--%>
                            </div>
                        </div>
                    </div>
                </div>



                 


         
        </div>
    </div>
    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

